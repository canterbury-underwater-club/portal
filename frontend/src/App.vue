<script setup lang="ts">
import { useRouteLoadingStore } from './stores/routeLoading'
</script>

<template>
  <VApp>
    <Loader v-if="useRouteLoadingStore().loading" />
    <RouterView />
  </VApp>
</template>
