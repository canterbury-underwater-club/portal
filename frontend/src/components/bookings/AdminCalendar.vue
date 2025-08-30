<!-- src/components/bookings/AdminCalendar.vue -->
<script setup lang="ts">
import type { BookingsAdminBookingsModelsBookingModel as AdminBooking } from '@/api/generated/v1'
import {
  addDays,
  endOfMonth,
  endOfWeek,
  format,
  isSameMonth,
  startOfMonth,
  startOfWeek,
} from 'date-fns'
import { computed } from 'vue'

type Props = {
  bookings: AdminBooking[]
  view: 'month' | 'week'
  anchor: Date
}
const props = defineProps<Props>()

type DayCell = { date: Date; iso: string; label: string; inMonth: boolean }

const gridDays = computed<DayCell[]>(() => {
  const weekStartsOn = 1 // Monday
  let start: Date
  let end: Date
  if (props.view === 'month') {
    start = startOfWeek(startOfMonth(props.anchor), { weekStartsOn })
    end = endOfWeek(endOfMonth(props.anchor), { weekStartsOn })
  } else {
    start = startOfWeek(props.anchor, { weekStartsOn })
    end = endOfWeek(props.anchor, { weekStartsOn })
  }

  const days: DayCell[] = []
  for (let d = start; d <= end; d = addDays(d, 1)) {
    days.push({
      date: d,
      iso: d.toISOString().slice(0, 10),
      label: format(d, 'd'),
      inMonth: isSameMonth(d, props.anchor),
    })
  }
  return days
})

// Index bookings per day for quick rendering
function overlapsDay(b: AdminBooking, isoDay: string) {
  // inclusive overlap
  return b.checkInDate <= isoDay && b.checkOutDate >= isoDay
}

const eventsByDay = computed<Record<string, AdminBooking[]>>(() => {
  const map: Record<string, AdminBooking[]> = {}
  for (const day of gridDays.value) {
    map[day.iso] = []
  }
  for (const b of props.bookings) {
    for (const day of gridDays.value) {
      if (overlapsDay(b, day.iso)) map[day.iso].push(b)
    }
  }
  // sort each day by start date then id for stable rows
  for (const iso in map) {
    map[iso].sort((a, b) => {
      if (a.checkInDate === b.checkInDate) return a.id.localeCompare(b.id)
      return a.checkInDate.localeCompare(b.checkInDate)
    })
  }
  return map
})
</script>

<template>
  <div class="calendar">
    <!-- Weekday headers -->
    <div class="grid grid-cols-7 text-xs font-semibold uppercase tracking-wide mb-1">
      <div class="p-2">Mon</div>
      <div class="p-2">Tue</div>
      <div class="p-2">Wed</div>
      <div class="p-2">Thu</div>
      <div class="p-2">Fri</div>
      <div class="p-2">Sat</div>
      <div class="p-2">Sun</div>
    </div>

    <!-- Day grid -->
    <div
      class="grid grid-cols-7 auto-rows-[minmax(110px,auto)] gap-[1px] bg-gray-200 rounded overflow-hidden"
    >
      <div
        v-for="day in gridDays"
        :key="day.iso"
        class="bg-white p-2 flex flex-col"
        :class="{ 'opacity-60': !day.inMonth }"
      >
        <div class="text-xs mb-2">
          <span
            class="inline-flex w-6 h-6 items-center justify-center rounded-full"
            :class="day.inMonth ? 'bg-gray-100' : 'bg-gray-50'"
          >
            {{ day.label }}
          </span>
        </div>

        <div class="flex-1 space-y-1 overflow-auto">
          <div
            v-for="b in eventsByDay[day.iso]"
            :key="b.id"
            class="text-[11px] leading-tight px-2 py-1 rounded border"
          >
            <div class="font-medium truncate">
              {{ b.groupName || 'Booking ' + b.id.slice(0, 6) }}
            </div>
            <div class="opacity-70 truncate">
              Rooms: {{ (b.rooms && b.rooms.join(', ')) || '—' }}
            </div>
            <div class="opacity-70">{{ b.checkInDate }} → {{ b.checkOutDate }}</div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
