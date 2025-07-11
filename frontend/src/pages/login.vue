<script setup lang="ts">
import banner from '@images/banner.png'

const route = useRoute()
const redirectUrl = computed(() => route.query.redirect?.toString())
const loading = ref(true)
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
        <FirebaseAuthUI :redirectUrl="redirectUrl" @ready="loading = false" />
      </VCardText>
    </VCard>
  </div>
</template>

<style lang="scss">
@use '@core/scss/template/pages/page-auth';
</style>
