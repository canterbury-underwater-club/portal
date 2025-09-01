<template>
  <VCalendar
    ref="cal"
    v-model="focusedDate"
    :view-mode="view"
    :events="calendarEvents"
    first-day-of-week="1"
    hide-week-number
    :intervals="1"
    :interval-format="''"
    :interval-duration="0"
    :interval-height="500"
  >
    <template #header="{ title, clickToday, clickNext, clickPrev }">
      <div class="d-flex align-center justify-space-between w-100 mb-1">
        <div class="d-flex align-center ga-2">
          <VBtn text="Today" variant="outlined" @click="clickToday" />
          <VBtn text="Next Booking" variant="outlined" @click="goToNextUpcomingBooking" />
          <VBtn icon="$prev" variant="text" @click="clickPrev" />
          <VBtn icon="$next" variant="text" @click="clickNext" />
          <span class="v-calendar-header__title">{{ title }}</span>
        </div>

        <VSelect
          v-model="view"
          :items="[
            { title: 'Month', value: 'month' },
            { title: 'Week', value: 'week' },
            { title: 'Day', value: 'day' },
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
      <VChip
        label
        variant="tonal"
        size="x-large"
        class="title-chip mb-1"
        :color="resolveEventColor(event)"
      >
        <strong>{{ event?.title }}</strong>
        <div class="text-sm ms-4">
          {{ resolveEventRooms(event) }}
        </div>
      </VChip>
    </template>
  </VCalendar>
</template>

<script setup lang="ts">
import type { Booking, BookingCalendarEvent, DateRange } from '@/types'
import { findNextUpcomingBooking } from '@/utils/bookingsUtils'
import { min } from 'lodash-es'
import { VCalendar } from 'vuetify/labs/VCalendar'

type ViewMode = 'month' | 'week' | 'day'

const props = withDefaults(
  defineProps<{
    bookings: Booking[]
  }>(),
  {
    bookings: () => [],
  },
)

const emit = defineEmits<{
  'date-range-change': [range: Required<DateRange>]
}>()

onMounted(() => goToNextUpcomingBooking())

const cal = ref<InstanceType<typeof VCalendar> | null>(null)
const focusedDate = ref<Date[]>([new Date(2025, 8, 1)])
const view = ref<ViewMode>('month')
const isMonthView = computed(() => view.value === 'month')

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

const calendarEvents = computed<BookingCalendarEvent[]>(() => {
  return props.bookings
    .flatMap((b) => b.toCalendarEvents())
    .sort((a, b) => (min(a.booking?.rooms ?? []) ?? 0) - (min(b.booking?.rooms ?? []) ?? 0))
})

function goToNextUpcomingBooking() {
  const nextBooking = findNextUpcomingBooking(props.bookings)

  if (nextBooking) focusedDate.value = [nextBooking.checkInDate]
}

function resolveEventColor(event: unknown): string {
  return (event as BookingCalendarEvent)?.color ?? 'primary'
}

function resolveEventRooms(event: unknown): string {
  const booking = (event as BookingCalendarEvent)?.booking
  return booking?.rooms?.length ? `Rooms: ${booking.rooms.join(', ')}` : ''
}

watch(dateRange, (range) => emit('date-range-change', range))
watch(calendarEvents, (newEvents, oldEvents) => {
  if (newEvents.length && !oldEvents.length) {
    goToNextUpcomingBooking()
  }
})
</script>

<style lang="scss" scoped>
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
