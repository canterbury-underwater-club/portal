<script setup lang="ts">
import type { Booking, BookingCalendarEvent, DateRange } from '@/types'
import {
  compareAsc,
  addDays as dfAddDays,
  format as dfFormat,
  eachDayOfInterval,
  parseISO,
  startOfDay,
} from 'date-fns'
import { onMounted } from 'vue'
import {
  InfiniteScrollSide,
  InfiniteScrollStatus,
} from 'vuetify/lib/components/VInfiniteScroll/VInfiniteScroll.mjs'

type LoaderFn = (range: Required<DateRange>) => Promise<Booking[]>

const props = withDefaults(
  defineProps<{
    loader: LoaderFn
    initialAnchorDate: Date | string
    pageDays?: number
    initialPastDays?: number
    initialFutureDays?: number
  }>(),
  {
    pageDays: 14,
    initialPastDays: 14,
    initialFutureDays: 14,
  },
)

const byDay = reactive<Record<string, BookingCalendarEvent[]>>({})
const dayKeys = ref<string[]>([])

const anchor = startOfDay(props.initialAnchorDate ? new Date(props.initialAnchorDate) : new Date())

let loadedFrom = startOfDay(dfAddDays(anchor, -props.initialPastDays))
let loadedTo = startOfDay(dfAddDays(anchor, props.initialFutureDays))

const scrollerRef = ref<HTMLElement | null>(null)
const dayElByKey = new Map<string, HTMLElement>()
const setDayEl = (key: string, el: HTMLElement | null) => {
  if (el) dayElByKey.set(key, el)
  else dayElByKey.delete(key)
}

const addDays = (d: Date, n: number) => startOfDay(dfAddDays(d, n))
const keyOf = (d: Date) => dfFormat(startOfDay(d), 'yyyy-MM-dd')

function ensureDayKeys(from: Date, to: Date) {
  const exist = new Set(dayKeys.value)
  for (const d of eachDayOfInterval({ start: from, end: to })) {
    const k = keyOf(d)
    if (!exist.has(k)) dayKeys.value.push(k)
  }
  dayKeys.value.sort((a, b) => compareAsc(parseISO(a), parseISO(b)))
}

async function loadRange(from: Date, to: Date) {
  ensureDayKeys(from, to)
  const bookings = await props.loader({ from, to })
  for (const k of dayKeys.value) byDay[k] = []
  for (const ev of bookings.flatMap((b) => b.toCalendarEvents())) {
    const k = keyOf(startOfDay(ev.start))
    if (byDay[k] && !byDay[k].some((e) => e.id === ev.id)) byDay[k].push(ev)
  }
}

function findNextFrom(date: Date): string | undefined {
  const fromKey = keyOf(date)
  for (const k of dayKeys.value) if (k >= fromKey && (byDay[k]?.length ?? 0) > 0) return k
  for (let i = dayKeys.value.length - 1; i >= 0; i--) {
    const k = dayKeys.value[i]
    if ((byDay[k]?.length ?? 0) > 0) return k
  }
}

function scrollToKey(k?: string) {
  if (!k) return
  const el = dayElByKey.get(k)
  if (!el) return
  el.scrollIntoView({ behavior: 'smooth' })
}

onMounted(async () => {
  await loadRange(loadedFrom, loadedTo)
  await nextTick()
  await new Promise((resolve) => setTimeout(resolve, 200))

  scrollToKey(findNextFrom(anchor))
})

async function loadOlderWindow() {
  loadedFrom = addDays(loadedFrom, -props.pageDays)
  await loadRange(loadedFrom, loadedTo)
}
async function loadNewerWindow() {
  loadedTo = addDays(loadedTo, props.pageDays)
  await loadRange(loadedFrom, loadedTo)
}

const onLoad = async ({
  side,
  done,
}: {
  side: InfiniteScrollSide
  done: (s: InfiniteScrollStatus) => void
}) => {
  try {
    if (side === 'start') await loadOlderWindow()
    if (side === 'end' || side === 'both') await loadNewerWindow()
    done('ok')
  } catch {
    done('error')
  }
}

const orderedDays = computed(() => dayKeys.value.map((k) => startOfDay(parseISO(k))))
</script>

<template>
  <VInfiniteScroll ref="scrollerRef" side="both" class="scroller" mode="intersect" :onLoad="onLoad">
    <div
      v-for="d in orderedDays"
      :key="keyOf(d)"
      :ref="(el) => setDayEl(keyOf(d), el as HTMLElement | null)"
    >
      <VCard class="mb-4" :elevation="1">
        <VCardTitle class="d-flex align-center justify-space-between">
          <div class="d-flex align-center ga-3">
            <span class="weekday text-body-2 text-medium-emphasis">
              {{ d.toLocaleDateString(undefined, { weekday: 'short' }) }}
            </span>
            <span class="text-h6">
              {{
                d.toLocaleDateString(undefined, {
                  day: 'numeric',
                  month: 'short',
                  year: 'numeric',
                })
              }}
            </span>
          </div>
          <VChip v-if="byDay[keyOf(d)]?.length" size="small" variant="tonal">
            {{ byDay[keyOf(d)].length }} booking{{ byDay[keyOf(d)].length > 1 ? 's' : '' }}
          </VChip>
        </VCardTitle>

        <template v-if="byDay[keyOf(d)]?.length">
          <VDivider />
          <VCardText>
            <VList density="comfortable">
              <VListItem v-for="ev in byDay[keyOf(d)]" :key="ev.id + '-' + keyOf(d)">
                <template #prepend><VAvatar size="10" :color="ev.color" /></template>
                <VListItemTitle class="font-weight-medium">{{ ev.title }}</VListItemTitle>
                <VListItemSubtitle>
                  Rooms: {{ ev.booking.rooms?.join(', ') || '—' }}
                </VListItemSubtitle>
              </VListItem>
            </VList>
          </VCardText>
        </template>
      </VCard>
    </div>
  </VInfiniteScroll>
</template>

<style lang="scss" scoped>
.scroller {
  max-height: calc(100vh - 310px);
}

.weekday {
  width: 1.5rem;
}
</style>
