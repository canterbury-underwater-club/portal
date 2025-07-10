<script setup lang="ts">
import banner from '@images/banner.png'

const route = useRoute()
const redirectUrl = computed(() => route.query.redirect?.toString())
const loading = ref(true)

let observer: MutationObserver | undefined

onMounted(() => {
  // Wait for the next tick to ensure FirebaseAuthUI is mounted/rendered
  setTimeout(() => {
    const container = document.querySelector('.firebaseui-container')
    if (!container) return

    observer = new MutationObserver(() => {
      if (container.querySelector('.firebaseui-idp-button')) {
        loading.value = false
        observer?.disconnect()
      }
    })
    observer.observe(container, { childList: true, subtree: true })

    // Edge case: in case button is already rendered before observer attaches
    if (container.querySelector('.firebaseui-idp-button')) {
      loading.value = false
      observer?.disconnect()
    }
  }, 0)
})

onBeforeUnmount(() => {
  observer?.disconnect()
})
</script>

<template>
  <Loader v-if="loading" />
  <div v-show="!loading" class="auth-wrapper d-flex align-center justify-center pa-4">
    <VCard class="auth-card pa-4 pt-7" max-width="448">
      <VCardItem class="justify-center">
        <VImg :src="banner" width="400" />
      </VCardItem>

      <VCardText class="pt-2">
        <h4 class="text-h4 mb-1 text-center">Dive in to continue</h4>
      </VCardText>

      <VCardText>
        <FirebaseAuthUI :redirectUrl="redirectUrl" />
      </VCardText>
    </VCard>
  </div>
</template>

<style lang="scss">
@use '@core/scss/template/pages/page-auth';
</style>
