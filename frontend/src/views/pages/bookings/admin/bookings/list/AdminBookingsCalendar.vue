<template>
  <BookingsCalendar :bookings="bookings" @date-range-change="onDateRangeChange" />
</template>

<script setup lang="ts">
import BookingsCalendar from '@/components/bookings/BookingsCalendar.vue'
import { useBookingsAdminStore } from '@/stores/bookings/admin'
import { BookingFromAdmin, DateRange } from '@/types'

const store = useBookingsAdminStore()

onMounted(async () => {
  await store.fetchBookings({ count: 20 })
})

const bookings = computed(() => store.allBookings.map(BookingFromAdmin))

async function onDateRangeChange(range: Required<DateRange>) {
  await store.fetchBookings({ dateRange: range })
}
</script>
