import { UserModel } from '@/api/generated/v1'
import { buildUsersApi } from '@/api/portal-api'

export const useUsersStore = defineStore('users', () => {
  const users = ref<UserModel[] | undefined>()

  async function fetchUsers() {
    const usersApi = await buildUsersApi()
    users.value = (await usersApi.v1UsersGet()).data.users
  }

  return {
    users,
    fetchUsers,
  }
})
