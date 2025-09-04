<template>
  <VDateInput
    v-model="selectedDates"
    label="Dates"
    multiple="range"
    input-format="dd/MM/yyyy"
    placeholder="Select Dates"
    prepend-icon=""
    :display-format="formatDate"
    :min="new Date().toISOString()"
  >
  </VDateInput>
  <VSelect
    label="Preferred Rooms"
    :items="availableRoomOptions"
    :no-data-text="!checkInDate ? 'Select dates first' : 'No rooms available for these dates'"
    :loading="checkingAvailability"
    multiple
  />
</template>
<script setup lang="ts">
import { useBookingsPublicStore } from '@/stores/bookings/public'
import { format } from 'date-fns'

const publicStore = useBookingsPublicStore()

const checkingAvailability = ref(false)
const selectedDates = ref<Date[]>([])
const formatCallCount = ref(0)
const availableRooms = ref<number[]>([])

const checkInDate = computed(() => selectedDates.value?.[0])
const checkOutDate = computed(() => selectedDates.value?.[selectedDates.value.length - 1])

const formatDate = (value: unknown) => {
  if (!value) return ''
  const dateStr = format(value as Date, 'd MMM yyyy')

  if (formatCallCount.value === 0) {
    formatCallCount.value++
    return `Check in: ${dateStr}`
  } else {
    formatCallCount.value = 0
    return `Check out: ${dateStr}`
  }
}

const availableRoomOptions = computed(() =>
  availableRooms.value.map((r) => ({
    title: `Room ${r}`,
    value: r,
  })),
)

watch(selectedDates, async () => {
  if (checkInDate.value !== checkOutDate.value) {
    checkingAvailability.value = true
    try {
      availableRooms.value = await publicStore.getAvailableRooms({
        from: checkInDate.value,
        to: checkOutDate.value,
      })
    } finally {
      checkingAvailability.value = false
    }
  }
})
</script>
