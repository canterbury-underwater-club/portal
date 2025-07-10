<script setup lang="ts">
import { useAuth } from '@/composables/useAuth'
import { Routes } from '@/plugins/router/constants'
import { useUserStore } from '@/stores'

const { user } = storeToRefs(useUserStore())
const userImage = computed(() => user.value?.photoURL ?? '')
</script>

<template>
  <VAvatar class="cursor-pointer" color="primary" variant="tonal">
    <VImg :src="userImage" width="40" />

    <!-- SECTION Menu -->
    <VMenu activator="parent" width="230" location="bottom end" offset="14px">
      <VList>
        <!-- User Avatar & Name -->
        <VListItem>
          <template #prepend>
            <VListItemAction start>
              <VAvatar color="primary" variant="tonal">
                <VImg :src="userImage" width="40" />
              </VAvatar>
            </VListItemAction>
          </template>

          <VListItemTitle class="font-weight-semibold">
            {{ `${user?.firstName} ${user?.lastName}` }}
          </VListItemTitle>
        </VListItem>
        <VDivider class="my-2" />

        <!-- Profile -->
        <VListItem link :to="{ name: Routes.AccountSettings }">
          <template #prepend>
            <VIcon class="me-2" icon="ri-user-line" size="22" />
          </template>

          <VListItemTitle>Profile</VListItemTitle>
        </VListItem>

        <!-- FAQ -->
        <VListItem link>
          <template #prepend>
            <VIcon class="me-2" icon="ri-question-line" size="22" />
          </template>

          <VListItemTitle>FAQ</VListItemTitle>
        </VListItem>

        <!-- Divider -->
        <VDivider class="my-2" />

        <!-- Sign out -->
        <VListItem @click="useAuth().signOut()">
          <template #prepend>
            <VIcon class="me-2" icon="ri-logout-box-r-line" size="22" />
          </template>

          <VListItemTitle>Sign out</VListItemTitle>
        </VListItem>
      </VList>
    </VMenu>
    <!-- !SECTION -->
  </VAvatar>
</template>
