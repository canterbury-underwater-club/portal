<script lang="ts" setup>
import { MembershipStatusModel } from '@/api/generated/v1'
import EditableAddressInput from '@/components/EditableAddressInput.vue'
import { Routes } from '@/plugins/router/constants'
import { useUsersStore } from '@/stores/usersStore'
import { resolveMembershipStatus, resolveUserPhoto } from '@/utils/userResolver'
import { emailRules, firstNameRules, nameRules, phoneRules } from '@/utils/validationRules'
import { debounce } from 'lodash-es'

const route = useRoute()
const router = useRouter()
const usersStore = useUsersStore()
const userId = computed(() => route.params.id as string)
const isEditing = computed(() => route.path.endsWith('/edit'))
const user = computed(() => usersStore.users?.find((u) => u.id === userId.value))

const firstName = ref('')
const lastName = ref('')
const emailAddress = ref('')
const mobilePhone = ref('')
const homePhone = ref('')
const address = ref('')
const membershipStatus = ref<MembershipStatusModel | undefined>(undefined)

const fields = {
  firstName,
  lastName,
  emailAddress,
  mobilePhone,
  homePhone,
  address,
  membershipStatus,
}

onMounted(async () => {
  if (!usersStore.users) await usersStore.fetchUsers()

  if (user.value) {
    firstName.value = user.value.firstName
    lastName.value = user.value.lastName ?? ''
    emailAddress.value = user.value.emailAddress
    mobilePhone.value = user.value.mobilePhone ?? ''
    homePhone.value = user.value.homePhone ?? ''
    address.value = user.value.address ?? ''
    membershipStatus.value = user.value.membershipStatus
  }
})

Object.entries(fields).forEach(([field, refVal]) => {
  watch(
    refVal,
    debounce((val) => {
      usersStore.updateUserPartial({ id: userId.value, [field]: val })
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
          <VAvatar :image="resolveUserPhoto(user)" size="64" />
        </VCol>
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

        <VCol cols="12">
          <EditableAddressInput v-model="address" label="Address" :editing="isEditing" />
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

        <VCol cols="12">
          <VBtn @click="toggleEdit" color="primary" class="ml-auto">
            {{ isEditing ? 'Done' : 'Edit' }}
          </VBtn>
        </VCol>
      </VRow>
    </VCardText>
  </VCard>
</template>
