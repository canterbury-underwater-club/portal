<script setup lang="ts">
// import { MemberStatusModel } from '@/api/generated/v1'
import { UserModel } from '@/api/generated/v1'
import { Routes } from '@/plugins/router/constants'
import { useUsersStore } from '@/stores/usersStore'
import {
  resolveUserIsActiveMember,
  resolveUserMembershipStatus,
  resolveUserPhoto,
} from '@/utils/userResolver'
import { VDataTable } from 'vuetify/components'

const router = useRouter()
const usersStore = useUsersStore()

const loading = computed(() => !usersStore.users)

import { ReadonlyDataTableHeader } from '@/plugins/vuetify/types'
import { useDisplay } from 'vuetify'
const { xs, smAndDown } = useDisplay()

onMounted(() => {
  usersStore.fetchUsers()
})

const page = ref(1)
const itemsPerPage = ref(50)
const search = ref('')
const membersOnly = ref(false)
const headers = computed<ReadonlyDataTableHeader[]>(() => {
  let result: ReadonlyDataTableHeader[] = [
    { key: 'photo', align: 'center' },
    { key: 'firstName', title: 'First name' },
    { key: 'lastName', title: 'Last name' },
  ]
  if (xs.value) return result

  result = [...result, { key: 'emailAddress', title: 'Email' }]
  if (smAndDown.value) return result

  result = [
    ...result,
    { key: 'phoneNumber', title: 'Phone number' },
    { key: 'membershipStatus', title: 'Membership status' },
  ]

  return result
})

const userData = computed(() => {
  return (usersStore.users ?? []).filter((u) => !membersOnly.value || resolveUserIsActiveMember(u))
})

const pageCount = computed(() => {
  return Math.ceil(userData.value.length / itemsPerPage.value)
})

const showPagination = computed(() => userData.value.length > itemsPerPage.value)

function goToPerson(_: unknown, row: { item: UserModel }) {
  router.push({ name: Routes.Person, params: { id: row.item.id } })
}
</script>

<template>
  <VSkeletonLoader :loading="loading" type="table">
    <VCard class="w-100 pb-3">
      <VCardText class="pb-0">
        <VTextField
          v-model="search"
          label="Search"
          prepend-inner-icon="ri-search-line"
          variant="outlined"
          hide-details
          single-line
        />

        <VCheckbox v-model="membersOnly" label="Members only" class="mt-2"></VCheckbox>
      </VCardText>

      <VDataTable
        v-model:page="page"
        :headers="headers"
        :items="userData"
        :search="search"
        :items-per-page="50"
        @click:row="goToPerson"
      >
        <template #[`item.photo`]="{ item: user }">
          <VAvatar :image="resolveUserPhoto(user)" />
        </template>
        <template #[`item.phoneNumber`]="{ item: user }">
          {{ user.mobilePhone ?? user.homePhone }}
        </template>
        <template #[`item.membershipStatus`]="{ item: user }">
          {{ resolveUserMembershipStatus(user) }}
        </template>

        <template v-slot:bottom>
          <div class="text-center pt-2" v-if="showPagination">
            <VPagination v-model="page" :length="pageCount" variant="plain"></VPagination>
          </div>
        </template>
      </VDataTable>
    </VCard>
  </VSkeletonLoader>
</template>
