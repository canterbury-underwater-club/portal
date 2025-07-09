import { auth } from '@/plugins/firebase'
import { User, signOut as fbSignOut, getIdToken, onAuthStateChanged } from 'firebase/auth'
import { ref } from 'vue'

export function useAuth() {
  const user = ref<User | null>(null)
  let _isInitialized = false

  let _ready: Promise<void> | null = null
  let _readyResolver: (() => void) | null = null

  const init = () => {
    if (!_isInitialized) {
      _ready = new Promise<void>((resolve) => {
        _readyResolver = resolve
      })

      onAuthStateChanged(auth, (fbUser) => {
        user.value = fbUser
        if (_readyResolver) {
          _readyResolver()
          _readyResolver = null
        }
      })
      _isInitialized = true
    }
  }

  // Ensure init runs once per app lifetime
  init()

  const waitForAuth = async () => {
    if (_ready) await _ready
  }

  const getUser = async (): Promise<User | null> => {
    await waitForAuth()
    return user.value
  }

  const getAccessToken = async (): Promise<string | undefined> => {
    if (!auth.currentUser) return undefined
    return await getIdToken(auth.currentUser)
  }

  const signOut = async () => {
    await fbSignOut(auth)
  }

  return {
    getAccessToken,
    getUser,
    signOut,
    user,
  }
}
