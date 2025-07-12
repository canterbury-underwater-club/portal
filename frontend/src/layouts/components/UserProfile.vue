<script setup lang="ts">
import { useAuth } from '@/composables/useAuth'
import { Routes } from '@/plugins/router/constants'
import { useCurrentUserStore } from '@/stores'
import { resolveUserDisplayName, resolveUserPhoto } from '@/utils/userResolver'

const { user } = storeToRefs(useCurrentUserStore())
const userPhoto = computed(() => resolveUserPhoto(user.value))
</script>

<template>
  <VAvatar class="cursor-pointer" color="primary" :variant="userPhoto ? 'text' : 'tonal'">
    <VAvatar :image="userPhoto" />

    <!-- SECTION Menu -->
    <VMenu activator="parent" width="230" location="bottom end" offset="14px">
      <VList>
        <!-- User Avatar & Name -->
        <VListItem>
          <template #prepend>
            <VListItemAction start>
              <VAvatar :image="userPhoto" />
            </VListItemAction>
          </template>

          <VListItemTitle class="font-weight-semibold">
            {{ resolveUserDisplayName(user) }}
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
