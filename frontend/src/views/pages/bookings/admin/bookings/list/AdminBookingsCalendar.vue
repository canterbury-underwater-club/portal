<template>
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
import { useBookingsAdminStore } from '@/stores/bookings/admin'
import { BookingCalendarEvent, DateRange } from '@/types'
import { min } from 'lodash-es'
import { VCalendar } from 'vuetify/labs/VCalendar'

type ViewMode = 'month' | 'week'

const store = useBookingsAdminStore()

const cal = ref<VCalendar | null>(null)
const focusedDate = ref([new Date()])

const view = ref<ViewMode>('month')
const isMonthView = computed(() => view.value === 'month')

onMounted(async () => {
  await store.fetchBookings({ count: 20 })
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
  await store.fetchBookings({ dateRange: range })
})

const calendarEvents = computed<BookingCalendarEvent[]>(() => {
  if (!dateRange.value) return []
  return store
    .bookingsForRange(dateRange.value)
    .flatMap((b) => b.toCalendarEvents())
    .sort((a, b) => (min(a.booking.rooms) ?? 0) - (min(b.booking.rooms) ?? 0))
})

function goToNextBooking() {
  const nextBooking = store.nextUpcomingBooking
  if (nextBooking) focusedDate.value = [nextBooking.checkInDate]
}

function resolveEventColor(event: unknown): string {
  return (event as BookingCalendarEvent).color
}

function resolveEventRooms(event: unknown): string {
  const { booking } = event as BookingCalendarEvent
  return `Rooms: ${booking.rooms.join(', ')}`
}
</script>

<style lang="scss" scoped>
.v-calendar {
  background-color: inherit;

  :deep(.v-calendar__container) {
    background-color: rgb(var(--v-theme-background));
  }
}

.title-chip {
  :deep(.v-chip__content) {
    flex-wrap: wrap;
  }
}
</style>
