<script setup lang="ts">
import { useCurrentUserStore } from '@/stores'

const router = useRouter()
const route = useRoute()
const redirectUrl = computed(() => route.query.redirect?.toString() ?? '/')

const { loading, user } = storeToRefs(useCurrentUserStore())

watch(loading, (isLoading) => {
  if (!isLoading && user.value) {
    router.replace(redirectUrl.value)
  }
})
</script>

<template>
  <Loader>
    <h6 class="text-base font-weight-semibold text-center">Signing in...</h6>
  </Loader>
</template>
