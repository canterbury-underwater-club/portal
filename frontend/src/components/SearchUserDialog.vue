<template>
  <VDialog v-model="open" max-width="560">
    <VCard>
      <VCardTitle />
      <VCardText>
        <VAutocomplete
          v-model="selectedUserId"
          label="Search"
          prepend-inner-icon="ri-search-line"
          :items="userOptions"
          item-value="id"
        />
      </VCardText>
      <VCardActions>
        <VBtn text="Select" variant="elevated" :disabled="!selectedUserId" @click="onSelectUser" />
        <VBtn text="Cancel" @click="open = false" />
      </VCardActions>
    </VCard>
  </VDialog>
</template>

<script setup lang="ts">
import { UsersModelsUserModel } from '@/api/generated/v1'
import { useUsersStore } from '@/stores/usersStore'

const usersStore = useUsersStore()
const open = defineModel<boolean>()
const emit = defineEmits<{
  (e: 'select-user', user: UsersModelsUserModel): void
}>()
const selectedUserId = ref<string | null>(null)

onMounted(() => {
  usersStore.fetchUsers()
})

const users = computed(() => usersStore.users ?? [])
const userOptions = computed(() =>
  users.value
    .sort((a, b) => a.firstName.localeCompare(b.firstName))
    .map((user) => ({
      ...user,
      title: `${user.firstName} ${user.lastName} (${user.emailAddress})`,
    })),
)

function onSelectUser() {
  const user = users.value.find((u) => u.id === selectedUserId.value)
  if (user) {
    emit('select-user', user)
    open.value = false
  }
}
</script>
