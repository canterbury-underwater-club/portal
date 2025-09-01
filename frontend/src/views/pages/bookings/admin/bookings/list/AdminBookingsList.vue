<template>
  <VSkeletonLoader v-if="!anchorDate" loading type="table" />
  <BookingsList v-if="anchorDate" :loader="loader" :initial-anchor-date="anchorDate" />
</template>

<script setup lang="ts">
import BookingsList from '@/components/bookings/BookingsList.vue'
import { useBookingsAdminStore } from '@/stores/bookings/admin'
import { BookingFromAdmin, type Booking, type DateRange } from '@/types'
import { fromServerDate } from '@/utils/dateConverter'

const store = useBookingsAdminStore()

const anchorDate = ref<Date | undefined>(undefined)

onMounted(async () => {
  try {
    const nextBooking = await store.fetchBookings({ count: 1 })
    const checkInDate = nextBooking.at(0)?.checkInDate
    if (checkInDate) anchorDate.value = fromServerDate(checkInDate)
  } catch {
    anchorDate.value = new Date()
  }
})

const loader = async (range: Required<DateRange>): Promise<Booking[]> => {
  const adminBookings = await store.fetchBookings({ dateRange: range })
  return adminBookings.map(BookingFromAdmin)
}
</script>
