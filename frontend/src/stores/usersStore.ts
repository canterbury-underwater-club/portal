import { UsersModelsUserModel as UserModel, UsersCreateContractsRequest } from '@/api/generated/v1'
import { buildUsersApi } from '@/api/portal-api'

export const useUsersStore = defineStore('users', () => {
  const byId = ref<Record<string, UserModel>>({})
  const loading = ref(false)

  const users = computed<UserModel[]>(() => Object.values(byId.value))

  async function fetchUsers() {
    if (loading.value) return
    loading.value = true
    try {
      const api = await buildUsersApi()
      const {
        data: { users: apiUsers },
      } = await api.v1UsersGet()
      byId.value = Object.fromEntries(apiUsers.map((u) => [u.id, u]))
    } finally {
      loading.value = false
    }
  }

  async function updateUserPartial(patch: Partial<UserModel> & { id: string }) {
    const user = byId.value[patch.id]
    if (user) {
      const usersApi = await buildUsersApi()
      await usersApi.v1UsersIdPatch(patch.id, patch)

      byId.value = { ...byId.value, [patch.id]: { ...user, ...patch } }
    }
  }

  async function createUser(payload: UsersCreateContractsRequest): Promise<UserModel> {
    const usersApi = await buildUsersApi()
    const {
      data: { user },
    } = await usersApi.v1UsersPost(payload)

    byId.value[user.id] = user

    return user
  }

  return {
    loading,
    users,
    createUser,
    fetchUsers,
    updateUserPartial,
  }
})
