<script lang="ts" setup>
import { MembershipStatusModel } from '@/api/generated/v1'
import EditableAddressInput from '@/components/EditableAddressInput.vue'
import { Routes } from '@/plugins/router/constants'
import { useUsersStore } from '@/stores/usersStore'
import { resolveMembershipStatus, resolveUserPhoto } from '@/utils/userResolver'
import {
  addressRules,
  emailRules,
  emergencyContactNameRules,
  firstNameRules,
  nameRules,
  occupationRules,
  phoneRules,
} from '@/utils/validationRules'
import { debounce } from 'lodash-es'

const route = useRoute()
const router = useRouter()
const usersStore = useUsersStore()
const userId = computed(() => route.params.id as string)
const isEditing = computed(() => route.path.endsWith('/edit'))
const user = computed(() => usersStore.users?.find((u) => u.id === userId.value))

const firstName = ref<string | null | undefined>(undefined)
const lastName = ref<string | null | undefined>(undefined)
const emailAddress = ref<string | null | undefined>(undefined)
const mobilePhone = ref<string | null | undefined>(undefined)
const homePhone = ref<string | null | undefined>(undefined)
const address = ref<string | null | undefined>(undefined)
const dateOfBirth = ref<string | null | undefined>(undefined)
const occupation = ref<string | null | undefined>(undefined)
const emergencyContactName = ref<string | null | undefined>(undefined)
const emergencyContactPhone = ref<string | null | undefined>(undefined)
const membershipStatus = ref<MembershipStatusModel | undefined>(undefined)
const membershipStartDate = ref<string | null | undefined>(undefined)
const membershipEndDate = ref<string | null | undefined>(undefined)

const fields = {
  firstName,
  lastName,
  emailAddress,
  mobilePhone,
  homePhone,
  address,
  dateOfBirth,
  occupation,
  emergencyContactName,
  emergencyContactPhone,
  membershipStatus,
  membershipStartDate,
  membershipEndDate,
}

onMounted(async () => {
  if (!usersStore.users) await usersStore.fetchUsers()

  if (user.value) {
    firstName.value = user.value.firstName ?? ''
    lastName.value = user.value.lastName ?? ''
    emailAddress.value = user.value.emailAddress ?? ''
    mobilePhone.value = user.value.mobilePhone ?? ''
    homePhone.value = user.value.homePhone ?? ''
    address.value = user.value.address ?? ''
    dateOfBirth.value = user.value.dateOfBirth ?? ''
    occupation.value = user.value.occupation ?? ''
    emergencyContactName.value = user.value.emergencyContactName ?? ''
    emergencyContactPhone.value = user.value.emergencyContactPhone ?? ''
    membershipStatus.value = user.value.membershipStatus ?? MembershipStatusModel.NonMember
    membershipStartDate.value = user.value.membershipStartDate ?? ''
    membershipEndDate.value = user.value.membershipEndDate ?? ''
  }
})

Object.entries(fields).forEach(([field, refVal]) => {
  watch(
    refVal,
    debounce((val, oldVal) => {
      if (isEditing && oldVal != undefined && val !== oldVal) {
        usersStore.updateUserPartial({ id: userId.value, [field]: val ?? '' })
      }
    }, 400),
  )
})

const membershipStatusOptions = Object.keys(MembershipStatusModel).map((key) => ({
  value: MembershipStatusModel[key as keyof typeof MembershipStatusModel],
  text: resolveMembershipStatus(MembershipStatusModel[key as keyof typeof MembershipStatusModel]),
}))

function toggleEdit() {
  if (isEditing.value) {
    // Done: go to /person/:id
    router.push({ name: Routes.Person, params: { id: userId.value } })
  } else {
    // Edit: go to /person/:id/edit
    router.push({ name: Routes.PersonEdit, params: { id: userId.value } })
  }
}
</script>

<template>
  <VCard>
    <VCardText>
      <VRow>
        <VCol cols="12">
          <div class="d-flex justify-space-between" style="max-width: 300px">
            <VAvatar :image="resolveUserPhoto(user)" size="64" />
            <VBtn v-if="!isEditing" @click="toggleEdit" color="primary" text="Edit" class="ms-10" />
          </div>
        </VCol>
      </VRow>
      <VRow>
        <VCol cols="12">
          <EditableTextField
            v-model="firstName"
            label="First name"
            :editing="isEditing"
            :rules="firstNameRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || lastName">
          <EditableTextField
            v-model="lastName"
            label="Last name"
            :editing="isEditing"
            :rules="nameRules"
          />
        </VCol>

        <VCol cols="12">
          <EditableTextField
            v-model="emailAddress"
            label="Email address"
            type="email"
            link-type="mailto"
            :editing="isEditing"
            :rules="emailRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || mobilePhone">
          <EditableTextField
            v-model="mobilePhone"
            label="Mobile phone"
            type="number"
            link-type="tel"
            :editing="isEditing"
            :rules="phoneRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || homePhone">
          <EditableTextField
            v-model="homePhone"
            label="Home phone"
            type="number"
            link-type="tel"
            :editing="isEditing"
            :rules="phoneRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || address">
          <EditableAddressInput
            v-model="address"
            label="Address"
            :editing="isEditing"
            :rules="addressRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || occupation">
          <EditableTextField
            v-model="occupation"
            label="Occupation"
            :editing="isEditing"
            :rules="occupationRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || emergencyContactName">
          <EditableTextField
            v-model="emergencyContactName"
            label="Emergency contact"
            :editing="isEditing"
            :rules="emergencyContactNameRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || emergencyContactPhone">
          <EditableTextField
            v-model="emergencyContactPhone"
            label="Emergency contact phone"
            type="number"
            link-type="tel"
            :editing="isEditing"
            :rules="phoneRules"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || dateOfBirth">
          <EditableDatePicker v-model="dateOfBirth" label="Date of birth" :editing="isEditing" />
        </VCol>

        <VCol cols="12">
          <EditableSelect
            v-model="membershipStatus"
            label="Membership status"
            :items="membershipStatusOptions"
            item-value="value"
            item-title="text"
            :editing="isEditing"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || membershipStartDate">
          <EditableDatePicker
            v-model="membershipStartDate"
            label="Membership start date"
            :editing="isEditing"
          />
        </VCol>

        <VCol cols="12" v-if="isEditing || membershipEndDate">
          <EditableDatePicker
            v-model="membershipEndDate"
            label="Membership end date"
            :editing="isEditing"
          />
        </VCol>

        <VCol cols="12">
          <VBtn v-if="isEditing" @click="toggleEdit" color="primary" text="Done" class="ml-auto" />
        </VCol>
      </VRow>
    </VCardText>
  </VCard>
</template>
