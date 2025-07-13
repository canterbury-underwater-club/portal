import { UserModel } from '@/api/generated/v1'
import { buildUsersApi } from '@/api/portal-api'

export const useUsersStore = defineStore('users', () => {
  const users = ref<UserModel[] | undefined>()

  async function fetchUsers() {
    const usersApi = await buildUsersApi()
    users.value = (await usersApi.v1UsersGet()).data.users
  }

  async function updateUserPartial(patch: Partial<UserModel> & { id: string }) {
    const usersApi = await buildUsersApi()
    await usersApi.v1UsersIdPatch(patch.id, patch)
    // Update locally
    if (users.value) {
      const idx = users.value.findIndex((u) => u.id === patch.id)
      if (idx !== -1) users.value[idx] = { ...users.value[idx], ...patch }
    }
  }

  return {
    users,
    fetchUsers,
    updateUserPartial,
  }
})
