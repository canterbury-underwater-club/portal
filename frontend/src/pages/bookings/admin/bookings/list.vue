<script setup lang="ts">
import { Routes } from '@/plugins/router/constants'
import { useBookingsAdminStore } from '@/stores/bookings/admin'
import AdminBookingsCalendar from '@/views/pages/bookings/admin/bookings/list/AdminBookingsCalendar.vue'
import AdminBookingsList from '@/views/pages/bookings/admin/bookings/list/AdminBookingsList.vue'

const route = useRoute()
const store = useBookingsAdminStore()
const loading = computed(() => store.loading)
const error = computed(() => store.error)

const activeTab = ref(route.params.tab)

const tabs = [
  { title: 'List', icon: 'ri-list-unordered', tab: 'list' },
  { title: 'Calendar', icon: 'ri-calendar-line', tab: 'calendar' },
]
</script>

<template>
  <VCard>
    <VCardTitle>
      <div class="d-flex align-center justify-space-between w-100 mb-2">
        <span class="text-h3">Bookings</span>
        <VBtn
          text="New Booking"
          class="text-uppercase"
          rounded="xl"
          :to="{ name: Routes.BookingsAdminBookingsCreate.name }"
        >
          <template #prepend>
            <VIcon icon="ri-add-line" size="x-large" />
          </template>
        </VBtn>
      </div>
    </VCardTitle>
    <VCardText>
      <VTabs v-model="activeTab" show-arrows class="v-tabs-pill">
        <VTab v-for="item in tabs" :key="item.icon" :value="item.tab">
          <VIcon size="20" start :icon="item.icon" />
          {{ item.title }}
        </VTab>
      </VTabs>
      <VAlert
        v-if="error"
        type="error"
        density="comfortable"
        title="Failed to load bookings"
        :text="String(error)"
      />

      <VProgressLinear v-if="loading" indeterminate />

      <VWindow v-model="activeTab" :touch="false">
        <VWindowItem value="list" class="mt-4">
          <AdminBookingsList />
        </VWindowItem>
        <VWindowItem value="calendar" class="mt-4">
          <AdminBookingsCalendar />
        </VWindowItem>
      </VWindow>
    </VCardText>
  </VCard>
</template>
