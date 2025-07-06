<script setup lang="ts">
import { Member } from '@/api/generated/v1'
import { buildMembersApi } from '@/api/portal-api'

const members = ref<Member[]>([])

onMounted(async () => {
  const api = await buildMembersApi()

  members.value = (await api.v1MembersGet()).data.members
})
</script>

<template>
  <VRow class="match-height">
    <VCol v-for="member in members" :key="member.id">
      {{ member.firstName }}
    </VCol>
  </VRow>
</template>
