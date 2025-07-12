import { UserModel } from '@/api/generated/v1'
import { buildUsersApi } from '@/api/portal-api'
import { auth } from '@/plugins/firebase'
import { onAuthStateChanged } from 'firebase/auth'
import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useCurrentUserStore = defineStore('current-user', () => {
  const user = ref<UserModel | null>(null)
  const loading = ref(true)

  onAuthStateChanged(auth, async (fbUser) => {
    loading.value = true
    try {
      if (fbUser) {
        const usersApi = await buildUsersApi()
        user.value = (await usersApi.v1UsersSignInPost()).data.user
      } else {
        user.value = null
      }
    } finally {
      loading.value = false
    }
  })

  const signOut = async () => {
    await auth.signOut()
    user.value = null
  }

  return {
    loading,
    user,
    signOut,
  }
})
