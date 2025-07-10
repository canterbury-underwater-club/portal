<script setup lang="ts">
import { UserModel } from '@/api/generated/v1'
import { buildUsersApi } from '@/api/portal-api'

const users = ref<UserModel[]>([])

async function loadUsers() {
  const api = await buildUsersApi()

  users.value = (await api.v1UsersGet()).data.users
}
</script>

<template>
  <VBtn @click="loadUsers">Load Users</VBtn>

  <VRow class="match-height">
    <VCol v-for="user in users" :key="user.id">
      {{ user.firstName }}
    </VCol>
  </VRow>
</template>
