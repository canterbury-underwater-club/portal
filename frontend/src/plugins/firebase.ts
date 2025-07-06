import { settingsService } from '@/utils/settingsService'
import { initializeApp } from 'firebase/app'
import { getAuth } from 'firebase/auth'

const firebaseConfig = {
  apiKey: settingsService.firebaseApiKey,
  authDomain: settingsService.firebaseAuthDomain,
  projectId: settingsService.firebaseProjectId,
  storageBucket: settingsService.firebaseStorageBucket,
  messagingSenderId: settingsService.firebaseMessagingSenderId,
  appId: settingsService.firebaseAppId,
}

const firebaseApp = initializeApp(firebaseConfig)
const auth = getAuth(firebaseApp)

export { auth, firebaseApp }
