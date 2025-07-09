// stores/user.ts
import { auth } from '@/plugins/firebase'
import { User as FirebaseUser, getIdToken, onAuthStateChanged } from 'firebase/auth'
import { defineStore } from 'pinia'
import { computed, ref } from 'vue'

export interface User extends FirebaseUser {
  roles: string[]
}

export const useUserStore = defineStore('user', () => {
  // Firebase user
  const firebaseUser = ref<FirebaseUser | null>(null)
  // API user (roles, etc)
  const apiUser = ref<{ roles: string[] } | null>(null)
  const loading = ref(true)

  onAuthStateChanged(auth, async (user) => {
    loading.value = true
    try {
      firebaseUser.value = user
      if (user) {
        // fetch apiUser goes here later
      } else {
        apiUser.value = null
      }
    } finally {
      loading.value = false
    }
  })

  const isAuthenticated = computed(() => !!firebaseUser.value)

  const getAccessToken = async () => {
    if (!auth.currentUser) return undefined
    return getIdToken(auth.currentUser)
  }

  const signOut = async () => {
    await auth.signOut()
    apiUser.value = null
  }

  const user = computed<User | null>(() => {
    if (!firebaseUser.value) return null
    // Merge roles into FirebaseUser object
    return {
      ...firebaseUser.value,
      roles: apiUser.value?.roles ?? [],
    }
  })

  return {
    user,
    isAuthenticated,
    loading,
    getAccessToken,
    signOut,
  }
})
