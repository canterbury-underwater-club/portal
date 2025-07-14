<script lang="ts" setup>
import VerticalNavLink from '@layouts/components/VerticalNavLink.vue'
</script>

<template>
  <VerticalNavLink
    :item="{
      title: 'People',
      icon: 'ri-team-line',
      to: '/people',
    }"
  />
</template>
