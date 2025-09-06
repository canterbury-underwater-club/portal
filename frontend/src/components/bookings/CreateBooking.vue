<template>
  <VCard title="Kaikōura Lodge Booking Request">
    <VCardText>
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
      <VBtn text="View lodge layout" variant="text" density="compact" class="mb-1" />
      <VSelect
        v-model="selectedRooms"
        label="Preferred Rooms"
        :items="availableRoomOptions"
        :no-data-text="!checkInDate ? 'Select dates first' : 'No rooms available for these dates'"
        :loading="checkingAvailability"
        multiple
      />
      <VAlert
        v-if="invalidSelectedRooms.length"
        :text="invalidRoomsMessage"
        color="warning"
        variant="outlined"
        closable
        density="compact"
        class="mt-1 invalid-rooms-alert"
        @click:close="invalidSelectedRooms = []"
      />
      <template v-if="availableRooms.length && AllRooms.some((r) => !availableRooms.includes(r))">
        <VLabel text="Can't see your preferred room?" />
        <VBtn
          text="Check availability"
          variant="text"
          density="compact"
          @click="showOccupancyCalendar = !showOccupancyCalendar"
        />
      </template>
    </VCardText>
  </VCard>

  <VDialog v-model="showOccupancyCalendar" scrollable>
    <VCard>
      <VCardTitle>
        <div class="d-flex align-center justify-space-between w-100 mb-1">
          Kaikōura Lodge Room Availability
          <VBtn
            icon="$close"
            variant="text"
            color="primary"
            @click="showOccupancyCalendar = false"
          />
        </div>
      </VCardTitle>
      <VCardText>
        <OccupancyCalendar :focused-date="checkInDate" />
      </VCardText>
    </VCard>
  </VDialog>
</template>
<script setup lang="ts">
import { AllRooms } from '@/constants/rooms'
import { useBookingsPublicStore } from '@/stores/bookings/public'
import { format } from 'date-fns'

const publicStore = useBookingsPublicStore()

const checkingAvailability = ref(false)
const showOccupancyCalendar = ref(false)
const selectedDates = ref<Date[]>([])
const formatCallCount = ref(0)
const availableRooms = ref<number[]>([])
const selectedRooms = ref<number[]>([])
const invalidSelectedRooms = ref<number[]>([])
const invalidRoomsMessage = computed(() => {
  switch (invalidSelectedRooms.value.length) {
    case 0:
      return
    case 1:
      return `Room ${invalidSelectedRooms.value[0]} is not available for the chosen dates`
    default:
      return `Room(s) ${invalidSelectedRooms.value.join(', ')} are not available for the chosen dates`
  }
})

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

      invalidSelectedRooms.value = selectedRooms.value.filter(
        (r) => !availableRooms.value.includes(r),
      )

      selectedRooms.value = selectedRooms.value.filter((r) => availableRooms.value.includes(r))
    } finally {
      checkingAvailability.value = false
    }
  }
})
</script>

<style lang="scss" scoped>
.invalid-rooms-alert {
  max-width: max-content;
}
</style>
