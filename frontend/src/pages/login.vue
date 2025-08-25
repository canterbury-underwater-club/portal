<template>
  <Loader v-if="loading">
    <h6 class="text-base font-weight-semibold text-center">Signing in…</h6>
  </Loader>

  <div v-else class="auth-wrapper d-flex align-center justify-center pa-4">
    <VCard class="auth-card pa-4 pt-7" max-width="448">
      <VCardItem class="justify-center">
        <VImg :src="banner" width="400" />
      </VCardItem>

      <VCardText class="pt-2">
        <h4 class="text-h4 mb-1 text-center">Dive in to continue</h4>
      </VCardText>

      <VCardText class="d-flex flex-column gap-3">
        <VBtn
          variant="outlined"
          :loading="redirecting"
          :disabled="redirecting"
          @click="signInWithGoogle"
        >
          <template #prepend>
            <VImg :src="GoogleLogo" alt="Google" width="24" contain />
          </template>
          Sign in with Google
        </VBtn>

        <VAlert v-if="error" type="error" variant="tonal" density="compact">
          {{ error }}
        </VAlert>
      </VCardText>
    </VCard>
  </div>
</template>

<script setup lang="ts">
import { auth } from '@/plugins/firebase'
import { Routes } from '@/plugins/router/constants'
import banner from '@images/banner.png'
import GoogleLogo from '@images/Google__G__logo.svg?url'
import {
  getRedirectResult,
  GoogleAuthProvider,
  onAuthStateChanged,
  signInWithRedirect,
} from 'firebase/auth'
import { computed, onBeforeUnmount, onMounted, ref } from 'vue'
import { useRoute, useRouter } from 'vue-router'

const route = useRoute()
const router = useRouter()

const redirectParam = computed(() => route.query.redirect?.toString() ?? '/dashboard')

const loading = ref(false)
const redirecting = ref(false)
const error = ref<string | null>(null)

const REDIRECT_FLAG = 'authRedirectPending'

const signInWithGoogle = async () => {
  try {
    redirecting.value = true
    sessionStorage.setItem(REDIRECT_FLAG, '1')
    await signInWithRedirect(auth, new GoogleAuthProvider())
  } catch (e: unknown) {
    redirecting.value = false
    sessionStorage.removeItem(REDIRECT_FLAG)
    error.value = (e as Error | undefined)?.message ?? 'Sign-in failed'
  }
}

let unsubscribe: (() => void) | null = null

onMounted(async () => {
  // If we see the flag at mount, we’re returning from provider redirect
  if (sessionStorage.getItem(REDIRECT_FLAG) === '1') {
    loading.value = true
  }

  try {
    const result = await getRedirectResult(auth)
    if (result?.user) {
      sessionStorage.removeItem(REDIRECT_FLAG)
      await router.replace({ name: Routes.LoginCallback, query: { redirect: redirectParam.value } })
      return
    }
  } catch (e: unknown) {
    error.value = (e as Error | undefined)?.message ?? 'Sign-in error'
  }

  unsubscribe = onAuthStateChanged(auth, async (user) => {
    if (user) {
      sessionStorage.removeItem(REDIRECT_FLAG)
      await router.replace({ name: Routes.LoginCallback, query: { redirect: redirectParam.value } })
    } else {
      // Not finishing a redirect after all—show the login UI
      loading.value = false
      redirecting.value = false
      sessionStorage.removeItem(REDIRECT_FLAG)
    }
  })
})

onBeforeUnmount(() => unsubscribe?.())
</script>

<style lang="scss">
@use '@core/scss/template/pages/page-auth';
</style>
