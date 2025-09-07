<template>
  <VCard title="Kaikōura Lodge Booking Request" max-width="1000">
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

      <VCard variant="flat">
        <VCardTitle class="d-flex align-center px-0">
          <VLabel
            text="Enter details for the person responsible for the group first"
            class="text-high-emphasis"
          />
          <VSpacer />
          <VBtn
            text="Search contacts"
            prepend-icon="ri-search-line"
            variant="outlined"
            @click="showSearchPrimaryContact = true"
          />
        </VCardTitle>
        <VCardText class="px-0">
          <VRow dense>
            <VCol cols="8">
              <VTextField v-model="primaryContactFullName" label="Full name" />
            </VCol>
            <VSpacer />
            <VCol cols="4">
              <VTextField
                v-model="primaryContactMembershipNumber"
                label="Membership number"
                type="number"
              />
            </VCol>
            <VCol cols="8">
              <VTextField
                v-model="primaryContactEmailAddress"
                label="Email address"
                :rules="emailRules"
              />
            </VCol>
            <VCol cols="4">
              <VTextField
                v-model="primaryContactMobilePhone"
                label="Mobile phone"
                type="number"
                :rules="phoneRules"
              />
            </VCol>
            <VCol cols="12">
              <EditableAddressInput
                v-model="primaryContactHomeAddress"
                label="Home address"
                :editing="true"
                :rules="addressRules"
              />
            </VCol>
            <VCol cols="12">
              <VSelect label="Age bracket" model-value="Adult" readonly />
            </VCol>
          </VRow>
        </VCardText>
      </VCard>

      <VBtn text="Add person"></VBtn>
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

  <SearchUserDialog v-model="showSearchPrimaryContact" @select-user="onSelectPrimaryContact" />
</template>
<script setup lang="ts">
import { BookingsModelsBookingAttendeeModel, UsersModelsUserModel } from '@/api/generated/v1'
import { AllRooms } from '@/constants/rooms'
import { useBookingsPublicStore } from '@/stores/bookings/public'
import { addressRules, emailRules, phoneRules } from '@/utils/validationRules'
import { format } from 'date-fns'

const publicStore = useBookingsPublicStore()

const selectedDates = ref<Date[]>([])
const selectedRooms = ref<number[]>([])
const primaryContactFullName = ref<string | undefined>()
const primaryContactMembershipNumber = ref<string | undefined>()
const primaryContactEmailAddress = ref<string | undefined>()
const primaryContactMobilePhone = ref<string | undefined>()
const primaryContactHomeAddress = ref<string | undefined>()

const attendees = ref<BookingsModelsBookingAttendeeModel[]>([])

const checkingAvailability = ref(false)
const showOccupancyCalendar = ref(false)
const showSearchPrimaryContact = ref(false)

const formatCallCount = ref(0)
const availableRooms = ref<number[]>([])

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

function onSelectPrimaryContact(user: UsersModelsUserModel) {
  primaryContactFullName.value = `${user.firstName} ${user.lastName}`.trim()
  primaryContactEmailAddress.value = user.emailAddress
  primaryContactMobilePhone.value = user.mobilePhone ?? user.homePhone ?? undefined
  primaryContactHomeAddress.value = user.address ?? undefined
}
</script>

<style lang="scss" scoped>
.invalid-rooms-alert {
  max-width: max-content;
}
</style>
