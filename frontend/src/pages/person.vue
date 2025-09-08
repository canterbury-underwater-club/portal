<script lang="ts" setup>
import {
  UsersModelsMembershipStatusModel as MembershipStatus,
  UsersCreateContractsRequest,
} from '@/api/generated/v1'
import EditableAddressInput from '@/components/EditableAddressInput.vue'
import { MAX_CARD_WIDTH } from '@/constants'
import { Routes } from '@/plugins/router/constants'
import { useUsersStore } from '@/stores/usersStore'
import { fromServerDate, toServerDate } from '@/utils/dateConverter'
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
import { VForm } from 'vuetify/components'

const route = useRoute()
const router = useRouter()
const usersStore = useUsersStore()

const isCreating = computed<boolean>(() => route.name === Routes.PeopleCreate.name)
const isViewing = computed<boolean>(
  () => route.name === Routes.PeopleView.name && user.value !== undefined,
)
const isEditing = computed<boolean>(
  () => route.name === Routes.PeopleEdit.name && user.value !== undefined,
)

const userId = computed(() => (isCreating.value ? undefined : (route.params.id as string)))
const user = computed(() =>
  userId.value ? usersStore.users?.find((u) => u.id === userId.value) : undefined,
)

const firstName = ref<string | null | undefined>(undefined)
const lastName = ref<string | null | undefined>(undefined)
const emailAddress = ref<string | null | undefined>(undefined)
const mobilePhone = ref<string | null | undefined>(undefined)
const homePhone = ref<string | null | undefined>(undefined)
const address = ref<string | null | undefined>(undefined)
const dateOfBirth = ref<Date | null | undefined>(undefined)
const occupation = ref<string | null | undefined>(undefined)
const emergencyContactName = ref<string | null | undefined>(undefined)
const emergencyContactPhone = ref<string | null | undefined>(undefined)
const membershipStatus = ref<MembershipStatus | undefined>(undefined)
const membershipStartDate = ref<Date | null | undefined>(undefined)
const membershipEndDate = ref<Date | null | undefined>(undefined)

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

const toServerFieldConverters: Partial<Record<keyof typeof fields, (v: unknown) => unknown>> = {
  dateOfBirth: (v) => (v ? toServerDate(v as Date) : v),
  membershipStartDate: (v) => (v ? toServerDate(v as Date) : v),
  membershipEndDate: (v) => (v ? toServerDate(v as Date) : v),
}

onMounted(async () => {
  if (isCreating.value) {
    membershipStatus.value = MembershipStatus.NonMember
    return
  }

  if (!usersStore.users) await usersStore.fetchUsers()

  if (user.value) {
    firstName.value = user.value.firstName ?? ''
    lastName.value = user.value.lastName ?? ''
    emailAddress.value = user.value.emailAddress ?? ''
    mobilePhone.value = user.value.mobilePhone ?? ''
    homePhone.value = user.value.homePhone ?? ''
    address.value = user.value.address ?? ''
    dateOfBirth.value = user.value.dateOfBirth ? fromServerDate(user.value.dateOfBirth) : undefined
    occupation.value = user.value.occupation ?? ''
    emergencyContactName.value = user.value.emergencyContactName ?? ''
    emergencyContactPhone.value = user.value.emergencyContactPhone ?? ''
    membershipStatus.value = user.value.membershipStatus ?? MembershipStatus.NonMember
    membershipStartDate.value = user.value.membershipStartDate
      ? fromServerDate(user.value.membershipStartDate)
      : undefined
    membershipEndDate.value = user.value.membershipEndDate
      ? fromServerDate(user.value.membershipEndDate)
      : undefined
  }
})

Object.entries(fields).forEach(([field, refVal]) => {
  watch(
    refVal,
    debounce((val, oldVal) => {
      if (isEditing.value && oldVal !== undefined && val !== oldVal && userId.value) {
        const convert =
          toServerFieldConverters[field as keyof typeof fields] ?? ((x: unknown) => x ?? '')
        usersStore.updateUserPartial({ id: userId.value, [field]: convert(val) })
      }
    }, 400),
  )
})

const membershipStatusOptions = Object.keys(MembershipStatus).map((key) => ({
  value: MembershipStatus[key as keyof typeof MembershipStatus],
  text: resolveMembershipStatus(MembershipStatus[key as keyof typeof MembershipStatus]),
}))

function toggleEdit() {
  if (isEditing.value) {
    router.push({ name: Routes.PeopleView.name, params: { id: userId.value } })
  } else {
    router.push({ name: Routes.PeopleEdit.name, params: { id: userId.value } })
  }
}

const formRef = ref<InstanceType<typeof VForm> | null>(null)
const saving = ref(false)
const errorMsg = ref<string | null>(null)

const trimOrEmpty = (v: string | null | undefined) => (typeof v === 'string' ? v.trim() : '')
const trimOrUndefined = (v: string | null | undefined) =>
  typeof v === 'string' ? v.trim() : undefined

async function saveNewPerson() {
  errorMsg.value = null
  if (!formRef.value) return
  const { valid } = await formRef.value.validate()
  if (!valid) return

  try {
    saving.value = true
    const payload: UsersCreateContractsRequest = {
      firstName: trimOrEmpty(firstName.value),
      lastName: trimOrUndefined(lastName.value),
      emailAddress: trimOrEmpty(emailAddress.value),
      mobilePhone: trimOrUndefined(mobilePhone.value),
      homePhone: trimOrUndefined(homePhone.value),
      address: trimOrUndefined(address.value),
      dateOfBirth: dateOfBirth.value ? toServerDate(dateOfBirth.value) : undefined,
      occupation: trimOrUndefined(occupation.value),
      emergencyContactName: trimOrUndefined(emergencyContactName.value),
      emergencyContactPhone: trimOrUndefined(emergencyContactPhone.value),
      membershipStatus: membershipStatus.value ?? MembershipStatus.NonMember,
      membershipStartDate: membershipStartDate.value
        ? toServerDate(membershipStartDate.value)
        : undefined,
      membershipEndDate: membershipEndDate.value
        ? toServerDate(membershipEndDate.value)
        : undefined,
      roles: [],
    }
    const created = await usersStore.createUser(payload)
    router.push({ name: Routes.PeopleView.name, params: { id: created.id } })
  } catch (e: unknown) {
    errorMsg.value = (e as Error | undefined)?.message ?? 'Failed to create person.'
  } finally {
    saving.value = false
  }
}

function cancelCreate() {
  router.push({ name: Routes.PeopleList.name })
}
</script>

<template>
  <VCard :max-width="MAX_CARD_WIDTH">
    <VCardTitle v-if="isCreating" class="mb-4"
      ><VIcon icon="ri-user-add-line" class="me-2" /> Add new person
    </VCardTitle>
    <VCardText>
      <VRow v-if="isViewing || isEditing" class="mb-2">
        <VCol cols="12">
          <div class="d-flex justify-space-between">
            <VAvatar :image="resolveUserPhoto(user)" size="64" />
            <VBtn
              v-if="isViewing"
              @click="toggleEdit"
              color="primary"
              text="Edit"
              variant="outlined"
            />
          </div>
        </VCol>
      </VRow>

      <VForm ref="formRef">
        <VRow :dense="isViewing || isCreating">
          <VCol cols="12">
            <EditableTextField
              v-model="firstName"
              label="First name"
              :editing="isEditing || isCreating"
              :rules="firstNameRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || lastName">
            <EditableTextField
              v-model="lastName"
              label="Last name"
              :editing="isEditing || isCreating"
              :rules="nameRules"
            />
          </VCol>

          <VCol cols="12">
            <EditableTextField
              v-model="emailAddress"
              label="Email address"
              type="email"
              link-type="mailto"
              :editing="isEditing || isCreating"
              :rules="emailRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || mobilePhone">
            <EditableTextField
              v-model="mobilePhone"
              label="Mobile phone"
              type="number"
              link-type="tel"
              :editing="isEditing || isCreating"
              :rules="phoneRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || homePhone">
            <EditableTextField
              v-model="homePhone"
              label="Home phone"
              type="number"
              link-type="tel"
              :editing="isEditing || isCreating"
              :rules="phoneRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || address">
            <EditableAddressInput
              v-model="address"
              label="Address"
              :editing="isEditing || isCreating"
              :rules="addressRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || occupation">
            <EditableTextField
              v-model="occupation"
              label="Occupation"
              :editing="isEditing || isCreating"
              :rules="occupationRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || emergencyContactName">
            <EditableTextField
              v-model="emergencyContactName"
              label="Emergency contact"
              :editing="isEditing || isCreating"
              :rules="emergencyContactNameRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || emergencyContactPhone">
            <EditableTextField
              v-model="emergencyContactPhone"
              label="Emergency contact phone"
              type="number"
              link-type="tel"
              :editing="isEditing || isCreating"
              :rules="phoneRules"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || dateOfBirth">
            <EditableDatePicker
              v-model="dateOfBirth"
              label="Date of birth"
              :editing="isEditing || isCreating"
            />
          </VCol>

          <VCol cols="12">
            <EditableSelect
              v-model="membershipStatus"
              label="Membership status"
              :items="membershipStatusOptions"
              item-value="value"
              item-title="text"
              :editing="isEditing || isCreating"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || membershipStartDate">
            <EditableDatePicker
              v-model="membershipStartDate"
              label="Membership start date"
              :editing="isEditing || isCreating"
            />
          </VCol>

          <VCol cols="12" v-if="isEditing || isCreating || membershipEndDate">
            <EditableDatePicker
              v-model="membershipEndDate"
              label="Membership end date"
              :editing="isEditing || isCreating"
            />
          </VCol>

          <VCol cols="12" class="d-flex">
            <!-- Footer actions by mode -->
            <template v-if="isEditing">
              <VBtn @click="toggleEdit" color="primary" text="Done" class="ml-auto" />
            </template>

            <template v-else-if="isCreating">
              <VAlert
                v-if="errorMsg"
                type="error"
                variant="tonal"
                class="mr-4"
                :text="errorMsg"
                closable
                @click:close="errorMsg = null"
              />
              <VSpacer />
              <VBtn variant="text" class="mr-2" @click="cancelCreate" :disabled="saving"
                >Cancel</VBtn
              >
              <VBtn color="primary" @click="saveNewPerson" :loading="saving">Save</VBtn>
            </template>
          </VCol>
        </VRow>
      </VForm>
    </VCardText>
  </VCard>
</template>
