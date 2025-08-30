<script lang="ts" setup>
import { Routes } from '@/plugins/router/constants'
import VerticalNavLink from '@layouts/components/VerticalNavLink.vue'
import VerticalNavSectionTitle from '@layouts/components/VerticalNavSectionTitle.vue'
</script>

<template>
  <VerticalNavLink
    :item="{
      title: 'People',
      icon: 'ri-team-line',
      to: Routes.PeopleList.path,
    }"
  />
  <VerticalNavSectionTitle
    :item="{
      heading: 'booking administration',
    }"
  />
  <VerticalNavLink
    :item="{
      title: 'Bookings',
      icon: 'ri-calendar-event-line',
      to: Routes.BookingsAdminBookingsList.path,
    }"
  />
</template>
