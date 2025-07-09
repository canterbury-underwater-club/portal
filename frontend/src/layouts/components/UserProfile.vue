<script setup lang="ts">
import { useAuth } from '@/composables/useAuth'
import { useUserStore } from '@/stores'

const router = useRouter()

const { user } = storeToRefs(useUserStore())
const userImage = computed(() => user.value?.photoURL ?? '')

const signOut = async () => {
  await useAuth().signOut()
  await router.replace({ path: '/', force: true })
}
</script>

<template>
  <VAvatar class="cursor-pointer" color="primary" variant="tonal">
    <VImg :src="userImage" />

    <!-- SECTION Menu -->
    <VMenu activator="parent" width="230" location="bottom end" offset="14px">
      <VList>
        <!-- User Avatar & Name -->
        <VListItem>
          <template #prepend>
            <VListItemAction start>
              <VAvatar color="primary" variant="tonal">
                <VImg :src="userImage" />
              </VAvatar>
            </VListItemAction>
          </template>

          <VListItemTitle class="font-weight-semibold"> {{ user?.displayName }} </VListItemTitle>
        </VListItem>
        <VDivider class="my-2" />

        <!-- Profile -->
        <VListItem link>
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
        <VListItem @click="signOut">
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
