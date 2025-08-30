<template>
  <VCard>
    <VCardTitle>
      <div class="d-flex align-center justify-space-between w-100 mb-2">
        <span class="text-h3">Bookings</span>
        <VBtn text="New Booking" class="text-uppercase" rounded="xl">
          <template #prepend>
            <VIcon icon="ri-add-line" size="x-large" />
          </template>
        </VBtn>
      </div>
    </VCardTitle>
    <VCardText>
      <VAlert
        v-if="error"
        type="error"
        density="comfortable"
        title="Failed to load bookings"
        :text="String(error)"
      />

      <VProgressLinear v-if="loading" indeterminate />

      <VCalendar
        ref="cal"
        v-model="focusedDate"
        :view-mode="view"
        :events="calendarEvents"
        first-day-of-week="1"
        hide-week-number
        :intervals="1"
        :interval-height="500"
      >
        <template #header="{ title, clickToday, clickNext, clickPrev }">
          <div class="d-flex align-center justify-space-between w-100 mb-1">
            <!-- left side -->
            <div class="d-flex align-center ga-2">
              <VBtn text="Today" variant="outlined" @click="clickToday" />
              <VBtn text="Next Booking" variant="outlined" @click="goToNextBooking" />
              <VBtn icon="$prev" variant="text" @click="clickPrev" />
              <VBtn icon="$next" variant="text" @click="clickNext" />
              <span class="v-calendar-header__title">{{ title }}</span>
            </div>

            <!-- right side -->
            <VSelect
              v-model="view"
              :items="[
                { title: 'Month', value: 'month' },
                { title: 'Week', value: 'week' },
              ]"
              density="compact"
              style="max-width: 140px"
              label="View"
              variant="outlined"
              base-color="primary"
            />
          </div>
        </template>
      </VCalendar>
    </VCardText>
  </VCard>
</template>

<script setup lang="ts">
import { useBookingsAdminStore } from '@/stores/bookings/admin'
import { CalendarEvent, DateRange } from '@/types'
import { fromServerDate } from '@/utils/dateConverter'
import { addDays } from 'date-fns'
import { VCalendar } from 'vuetify/labs/VCalendar'

type ViewMode = 'month' | 'week'
const INCLUSIVE_END = false // VCalendar usually treats end as exclusive for multi-day
const cal = ref<VCalendar | null>(null)

const focusedDate = ref([new Date()])

const store = useBookingsAdminStore()

const view = ref<ViewMode>('month')
const isMonthView = computed(() => view.value === 'month')

onMounted(async () => {
  await store.fetchList({ count: 20 })
  goToNextBooking()
})

const dateRange = computed<Required<DateRange>>(() => {
  if (!exposedDays.value.length) return { from: new Date(), to: new Date() }

  const sorted = [...exposedDays.value].sort((a, b) => a.getTime() - b.getTime())

  return {
    from: sorted[0],
    to: sorted[sorted.length - 1],
  }
})

const exposedDays = computed<Date[]>(() => {
  const days = isMonthView.value ? cal.value?.daysInMonth : cal.value?.daysInWeek
  return (days ?? []).map((d) => d.date)
})

watch(dateRange, async (range) => {
  await store.fetchList({ dateRange: range })
})

const calendarEvents = computed<CalendarEvent<unknown>[]>(() => {
  if (!dateRange.value) return []
  return store.eventsForRange(dateRange.value).map((e) => ({
    id: e.id,
    title: e.title,
    start: e.start,
    end: INCLUSIVE_END ? e.end : addDays(e.end, 1),
    rooms: e.rooms,
    source: e.source,
  }))
})

const loading = computed(() => store.loading)
const error = computed(() => store.error)

function goToNextBooking() {
  const nextBooking = store.nextBooking
  if (nextBooking) focusedDate.value = [fromServerDate(nextBooking.checkInDate)]
}
</script>

<style lang="scss" scoped>
.v-calendar {
  background-color: inherit;

  :deep(.v-calendar__container) {
    background-color: rgb(var(--v-theme-background));
  }
}
</style>
