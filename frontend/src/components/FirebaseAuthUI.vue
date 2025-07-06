<template>
  <div>
    <div ref="firebaseuiRef" />
  </div>
</template>

<script setup lang="ts">
import { auth } from '@/plugins/firebase'
import * as firebaseui from 'firebaseui'
import 'firebaseui/dist/firebaseui.css'
import { onMounted, onUnmounted, ref } from 'vue'

const firebaseuiRef = ref<HTMLDivElement | null>(null)
let ui: firebaseui.auth.AuthUI | null = null

const props = defineProps<{ signInSuccessUrl?: string }>()

const router = useRouter()

onMounted(() => {
  if (!ui) {
    ui = new firebaseui.auth.AuthUI(auth)
  }
  if (firebaseuiRef.value) {
    ui.start(firebaseuiRef.value, {
      signInFlow: 'popup',
      signInOptions: [
        'google.com',
        // Add others as needed
      ],
      callbacks: {
        signInSuccessWithAuthResult: (authResult: unknown, redirectUrl?: string) => {
          console.log(JSON.stringify(authResult))
          // Use your desired redirect URL (from prop, query, etc.)
          // This could be from props, or your computed value:
          router.replace(props.signInSuccessUrl ?? redirectUrl ?? '/')
          return false // Prevents FirebaseUI from doing its own redirect
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
