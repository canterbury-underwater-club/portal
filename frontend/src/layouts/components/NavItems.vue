<script lang="ts" setup>
import { Routes } from '@/plugins/router/constants'
import VerticalNavLink from '@layouts/components/VerticalNavLink.vue'
</script>

<template>
  <VerticalNavLink
    :item="{
      title: 'People',
      icon: 'ri-team-line',
      to: Routes.PeopleList.path,
    }"
  />
</template>
