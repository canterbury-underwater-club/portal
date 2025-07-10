import { UserModel } from '@/api/generated/v1'
import { buildUsersApi } from '@/api/portal-api'
import { auth } from '@/plugins/firebase'
import { User as FirebaseUser, onAuthStateChanged } from 'firebase/auth'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export interface User extends UserModel {
  photoURL?: string
}

export const useUserStore = defineStore('user', () => {
  const firebaseUser = ref<FirebaseUser | null>(null)
  const apiUser = ref<UserModel | null>(null)
  const loading = ref(true)

  onAuthStateChanged(auth, async (fbUser) => {
    loading.value = true
    try {
      firebaseUser.value = fbUser
      if (fbUser) {
        const usersApi = await buildUsersApi()
        apiUser.value = (await usersApi.v1UsersSignInPost()).data.user
      } else {
        apiUser.value = null
      }
    } finally {
      loading.value = false
    }
  })

  const signOut = async () => {
    await auth.signOut()
    apiUser.value = null
  }

  const user = computed<User | undefined>(() => {
    if (!apiUser.value) return undefined

    return {
      ...apiUser.value,
      photoURL: firebaseUser.value?.photoURL ?? undefined,
    }
  })

  return {
    loading,
    user,
    signOut,
  }
})
