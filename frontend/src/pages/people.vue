<script setup lang="ts">
// import { MemberStatusModel } from '@/api/generated/v1'
import { UserModel } from '@/api/generated/v1'
import { Routes } from '@/plugins/router/constants'
import { ReadonlyDataTableHeader } from '@/plugins/vuetify/types'
import { useUsersStore } from '@/stores/usersStore'
import { resolveUserMembershipStatus, resolveUserPhoto } from '@/utils/userResolver'
import { VDataTable } from 'vuetify/components'

const router = useRouter()
const usersStore = useUsersStore()

const loading = computed(() => !usersStore.users)

onMounted(() => {
  usersStore.fetchUsers()
})

const headers: ReadonlyDataTableHeader[] = [
  { key: 'photo', align: 'center' },
  { key: 'firstName', title: 'First name' },
  { key: 'lastName', title: 'Last name' },
  { key: 'emailAddress', title: 'Email' },
  { key: 'phoneNumber', title: 'Phone number' },
  { key: 'membershipStatus', title: 'Membership status' },
]

const userData = computed(() => {
  return usersStore.users ?? []
})

function goToPerson(_: unknown, row: { item: UserModel }) {
  router.push({ name: Routes.Person, params: { id: row.item.id } })
}
</script>

<template>
  <VSkeletonLoader :loading="loading" type="table">
    <VCard class="w-100">
      <VCardText>
        <VDataTable
          :headers="headers"
          :items="userData"
          @click:row="goToPerson"
          :items-per-page="50"
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
        </VDataTable>
      </VCardText>
    </VCard>
  </VSkeletonLoader>
</template>
