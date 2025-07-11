<script setup lang="ts">
// import { MemberStatusModel } from '@/api/generated/v1'
import { Routes } from '@/plugins/router/constants'
import { ReadonlyDataTableHeader } from '@/plugins/vuetify/types'
import { useUsersStore } from '@/stores/usersStore'
import { VDataTable } from 'vuetify/components'

const router = useRouter()
const usersStore = useUsersStore()

// const statuses = computed(() => Object.values(MemberStatusModel))
// const updatingRoles = ref(false)

const loading = computed(() => !usersStore.users)

onMounted(async () => {
  await usersStore.fetchUsers()
})

const headers: ReadonlyDataTableHeader[] = [
  { key: 'firstName', title: 'First Name' },
  { key: 'lastName', title: 'Last Name' },
  { key: 'emailAddress', title: 'Email Address' },
  { key: 'membershipStatus', title: 'Membership Status' },
  { key: 'id', align: 'center' },
]

const userData = computed(() => {
  return usersStore.users ?? []
})

function goToPerson(id: string) {
  router.push({ name: Routes.Person, params: { id } })
}

// const updateUserRoles = async (
//   user: OrganisationMember,
//   newRoles: OrganisationMemberRolesEnum[],
// ) => {
//   const initialUserRoles = [...user.roles]

//   try {
//     user.roles = newRoles
//     const addedRoles = newRoles.filter((role) => !initialUserRoles.includes(role))
//     const addTasks = addedRoles.map(async (role) => {
//       await organisationStore.addOrganisationMemberRole(user, role)
//     })

//     const removedRoles = initialUserRoles.filter((role) => !newRoles.includes(role))
//     const removeTasks = removedRoles.map(async (role) => {
//       await organisationStore.deleteOrganisationMemberRole(user, role)
//     })

//     updatingRoles.value = true
//     await Promise.all([...addTasks, ...removeTasks])
//   } catch (err) {
//     user.roles = initialUserRoles
//     throw err
//   } finally {
//     user.roles = Array.from(new Set(user.roles))
//     updatingRoles.value = false
//   }
// }
</script>

<template>
  <VSkeletonLoader :loading="loading" type="table">
    <VCard class="w-100">
      <VCardText>
        <VDataTable :headers="headers" :items="userData">
          <template #[`item.id`]="{ item: user }">
            <VBtn
              icon="ri-edit-line"
              color="primary"
              variant="text"
              @click="goToPerson(user.id)"
            ></VBtn>
          </template>
        </VDataTable>
      </VCardText>
    </VCard>
  </VSkeletonLoader>
</template>
