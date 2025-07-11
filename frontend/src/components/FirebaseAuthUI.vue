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
const emit = defineEmits(['ready'])
const router = useRouter()

let observer: MutationObserver | null = null

onMounted(() => {
  const existingUi = firebaseui.auth.AuthUI.getInstance()
  if (existingUi) existingUi.delete()
  ui = new firebaseui.auth.AuthUI(auth)

  nextTick(() => {
    if (firebaseuiRef.value) {
      ui?.start(firebaseuiRef.value, {
        signInFlow: 'redirect',
        signInOptions: [GoogleAuthProvider.PROVIDER_ID],
        callbacks: {
          signInSuccessWithAuthResult: () => {
            router.replace({
              name: Routes.LoginCallback,
              query: { redirect: props.redirectUrl ?? '/dashboard' },
            })
            return false
          },
          signInFailure: (error: firebaseui.auth.AuthUIError) => {
            console.error(`sign in failure: ${error.message}`)
          },
        },
        credentialHelper: firebaseui.auth.CredentialHelper.NONE,
      })
      // MutationObserver to emit when providers are ready
      observer = new MutationObserver(() => {
        const container = firebaseuiRef.value
        if (container && container.querySelector('.firebaseui-idp-button')) {
          emit('ready')
          observer?.disconnect()
        }
      })
      if (firebaseuiRef.value) {
        observer.observe(firebaseuiRef.value, { childList: true, subtree: true })
        // Immediate check in case button is already rendered
        if (firebaseuiRef.value.querySelector('.firebaseui-idp-button')) {
          emit('ready')
          observer.disconnect()
        }
      }
    }
  })
})

onUnmounted(() => {
  observer?.disconnect()

  if (ui) {
    ui.delete()
    ui = null
  }
})
</script>
