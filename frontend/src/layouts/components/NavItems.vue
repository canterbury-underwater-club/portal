<script lang="ts" setup>
import VerticalNavSectionTitle from '@/@layouts/components/VerticalNavSectionTitle.vue'
import VerticalNavLink from '@layouts/components/VerticalNavLink.vue'
</script>

<template>
  <!-- Apps & Pages -->
  <VerticalNavSectionTitle
    :item="{
      heading: 'Apps & Pages',
    }"
  />

  <VerticalNavLink
    :item="{
      title: 'Dashboard',
      icon: 'ri-home-line',
      to: '/dashboard',
    }"
  />

  <VerticalNavLink
    :item="{
      title: 'People',
      icon: 'ri-team-line',
      to: '/people',
    }"
  />
</template>
