<script lang="ts" setup>
const firstName = ref('')
const email = ref('')
const mobile = ref<number>()
const checkbox = ref(false)
</script>

<template>
  <VForm @submit.prevent>
    <VRow>
      <VCol cols="12">
        <VTextField
          v-model="firstName"
          prepend-inner-icon="ri-user-line"
          label="First Name"
        />
      </VCol>

      <VCol cols="12">
        <VTextField
          v-model="email"
          prepend-inner-icon="ri-mail-line"
          label="Email"
          type="email"
        />
      </VCol>

      <VCol cols="12">
        <VTextField
          v-model="mobile"
          prepend-inner-icon="ri-smartphone-line"
          label="Mobile"
          type="number"
        />
      </VCol>

      <VCol cols="12">
        <VCheckbox v-model="checkbox" label="Remember me" />
      </VCol>

      <VCol cols="12">
        <VBtn type="submit" class="me-2"> Save </VBtn>
      </VCol>
    </VRow>
  </VForm>
</template>
