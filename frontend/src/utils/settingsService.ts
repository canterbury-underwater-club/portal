class SettingsService {
  get isDevelopment() {
    return import.meta.env.MODE === 'development'
  }

  get apiBasePath() {
    return import.meta.env.VITE_API_BASE_PATH
  }

  get firebaseApiKey() {
    return import.meta.env.VITE_FIREBASE_API_KEY
  }

  get firebaseAuthDomain() {
    return import.meta.env.VITE_FIREBASE_AUTH_DOMAIN
  }

  get firebaseProjectId() {
    return import.meta.env.VITE_FIREBASE_PROJECT_ID
  }

  get firebaseStorageBucket() {
    return import.meta.env.VITE_FIREBASE_STORAGE_BUCKET
  }

  get firebaseMessagingSenderId() {
    return import.meta.env.VITE_FIREBASE_MESSAGING_SENDER_ID
  }

  get firebaseAppId() {
    return import.meta.env.VITE_FIREBASE_APP_ID
  }
}

export const settingsService = new SettingsService()
