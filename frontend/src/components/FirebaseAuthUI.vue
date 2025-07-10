<template>
  <div>
    <div ref="firebaseuiRef" />
  </div>
</template>

<script setup lang="ts">
import { auth } from '@/plugins/firebase'
import { Routes } from '@/plugins/router/constants'
import { GoogleAuthProvider } from 'firebase/auth'
import * as firebaseui from 'firebaseui'
import 'firebaseui/dist/firebaseui.css'
import { onMounted, onUnmounted, ref } from 'vue'

const firebaseuiRef = ref<HTMLDivElement | null>(null)
let ui: firebaseui.auth.AuthUI | null = null

const props = defineProps<{ redirectUrl?: string }>()
const router = useRouter()

onMounted(() => {
  const existingUi = firebaseui.auth.AuthUI.getInstance()
  if (existingUi) {
    existingUi.delete()
  }
  ui = new firebaseui.auth.AuthUI(auth)

  if (firebaseuiRef.value) {
    ui.start(firebaseuiRef.value, {
      signInFlow: 'redirect',
      signInOptions: [
        GoogleAuthProvider.PROVIDER_ID,
        // Add others as needed
      ],
      callbacks: {
        signInSuccessWithAuthResult: () => {
          router.replace({
            name: Routes.LoginCallback,
            query: { redirect: props.redirectUrl ?? '/dashboard' },
          })
          return false // Prevents FirebaseUI from doing its own redirect
        },
        signInFailure: (error: firebaseui.auth.AuthUIError) => {
          console.error(`sign in failure: ${error.message}`)
        },
      },
      credentialHelper: firebaseui.auth.CredentialHelper.NONE,
    })
  }
})

onUnmounted(() => {
  if (ui) {
    ui.delete()
    ui = null
  }
})
</script>
