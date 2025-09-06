<template>
  <VProgressLinear :indeterminate="loading" class="mb-3" :class="{ 'hide-loader': !loading }" />
  <VCalendar
    ref="cal"
    v-model="internalFocusedDate"
    :view-mode="view"
    :events="calendarEvents"
    hide-week-number
    show-adjacent-months
    first-day-of-week="1"
    :intervals="1"
    :interval-start="23"
    :interval-height="528"
  >
    <template #header="{ title, clickToday, clickNext, clickPrev }">
      <div class="d-flex align-center justify-space-between w-100 mb-1">
        <div class="d-flex align-center ga-2">
          <VSelect
            v-model="selectedRooms"
            :items="roomOptions"
            label="Rooms"
            multiple
            clearable
            placeholder="All rooms"
            persistent-placeholder
            density="compact"
            width="170"
          />
          <VBtn text="Today" variant="outlined" @click="clickToday" />
          <VBtn icon="$prev" variant="text" @click="clickPrev" />
          <VBtn icon="$next" variant="text" @click="clickNext" />
          <span class="v-calendar-header__title">{{ title }}</span>
        </div>

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

    <template #day-event="{ event }">
      <div class="room-list">
        <VChip
          label
          variant="tonal"
          size="x-large"
          class="title-chip mb-1"
          :color="resolveEventColor(event)"
        >
          <strong>{{ event?.title }}</strong>
        </VChip>
      </div>
    </template>
    <template #interval-event="{ event }">
      <div class="room-list">
        <VChip
          label
          variant="tonal"
          size="x-large"
          class="title-chip mb-1"
          :color="resolveEventColor(event)"
        >
          <strong>{{ event?.title }}</strong>
        </VChip>
      </div>
    </template>
  </VCalendar>
</template>

<script setup lang="ts">
import { AllRooms } from '@/constants/rooms'
import { OccupancyStatus, useBookingsPublicStore } from '@/stores/bookings/public'
import type { DateRange } from '@/types'
import { addDays, subMinutes } from 'date-fns'
import { VCalendar } from 'vuetify/labs/VCalendar'

type ViewMode = 'month' | 'week'
interface OccupancyCalendarEvent {
  title: string
  start: Date // 00:00:00 for the booking start
  end: Date // 23:59:59 for the booking end
  room: number
  status: OccupancyStatus
}

const props = defineProps<{ focusedDate?: Date }>()
const internalFocusedDate = ref([props.focusedDate])

const cal = ref<InstanceType<typeof VCalendar> | null>(null)
const view = ref<ViewMode>('month')
const isMonthView = computed(() => view.value === 'month')
const selectedRooms = ref<number[]>([])
const roomOptions = computed(() =>
  AllRooms.map((r) => ({
    title: `Room ${r}`,
    value: r,
  })),
)

const displayedRooms = computed(() => (selectedRooms.value.length ? selectedRooms.value : AllRooms))

const store = useBookingsPublicStore()

const { loading } = storeToRefs(store)

const exposedDays = computed<Date[]>(() => {
  const days = isMonthView.value ? cal.value?.daysInMonth : cal.value?.daysInWeek
  return (days ?? []).map((d) => d.date)
})

const dateRange = computed<Required<DateRange>>(() => {
  if (!exposedDays.value.length) {
    const today = new Date()
    return { from: today, to: today }
  }
  const sorted = [...exposedDays.value].sort((a, b) => a.getTime() - b.getTime())
  return { from: sorted[0], to: sorted[sorted.length - 1] }
})

const COLOR_AVAILABLE = '#16a34a' // green-ish
const COLOR_PENDING = '#f59e0b' // amber-ish
const COLOR_BOOKED = '#dc2626' // red-ish

const calendarEvents = computed<OccupancyCalendarEvent[]>(() => {
  const days = exposedDays.value
  if (!days.length) return []

  return days
    .flatMap((day) => {
      const dayOccupancy = store.getOccupancy(day)

      return displayedRooms.value.map((room) => {
        let status: OccupancyStatus = OccupancyStatus.Available

        if (dayOccupancy) {
          const roomOccupancy = dayOccupancy.rooms.find((r) => r.room === room)
          if (roomOccupancy) {
            status = roomOccupancy.status
          }
        }

        return {
          title: `Room ${room}`,
          start: day,
          end: subMinutes(addDays(day, 1), 1),
          room,
          status,
        }
      })
    })
    .sort((a, b) => a.start.getTime() - b.start.getTime() || a.room - b.room)
})

function resolveEventColor(event: unknown): string {
  const status = (event as OccupancyCalendarEvent)?.status
  switch (status) {
    case OccupancyStatus.Available:
      return COLOR_AVAILABLE
    case OccupancyStatus.Pending:
      return COLOR_PENDING
    case OccupancyStatus.Booked:
      return COLOR_BOOKED
  }
}

watch(dateRange, async (range) => {
  await store.fetchOccupancy(range)
})
</script>

<style scoped lang="scss">
.room-list {
  display: flex;
  flex-direction: column;
  gap: 6px;
  margin-inline: 10px;
}

.hide-loader {
  :deep(.v-progress-linear__background) {
    background-color: transparent !important;
  }
}

.v-calendar {
  background-color: inherit;

  :deep(.v-calendar__container) {
    background-color: rgb(var(--v-theme-background));

    .v-calendar-day__row-with-label {
      display: block;

      .v-calendar-day__row-label {
        display: none;
      }

      .v-calendar-day__row-hairline {
        display: none;
      }
    }
  }
}
.title-chip {
  :deep(.v-chip__content) {
    flex-wrap: wrap;
  }
}
</style>
