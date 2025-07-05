<script lang="ts" setup>
const firstName = ref('')
const lastName = ref('')
const city = ref('')
const country = ref('')
const company = ref('')
const email = ref('')
const checkbox = ref(false)
</script>

<template>
  <VForm @submit.prevent="() => {}">
    <VRow>
      <!-- First Name -->
      <VCol cols="12" md="6">
        <VTextField v-model="firstName" label="First Name" placeholder="John" />
      </VCol>

      <!-- t Name -->
      <VCol cols="12" md="6">
        <VTextField v-model="lastName" label="Last Name" placeholder="Doe" />
      </VCol>

      <!-- il -->
      <VCol cols="12" md="6">
        <VTextField v-model="email" label="Email" placeholder="johndoe@email.com" />
      </VCol>

      <!-- y -->
      <VCol cols="12" md="6">
        <VTextField v-model="city" label="City" placeholder="New York" />
      </VCol>

      <!-- ntry -->
      <VCol cols="12" md="6">
        <VTextField v-model="country" label="Country" placeholder="United States" />
      </VCol>

      <!-- pany -->
      <VCol cols="12" md="6">
        <VTextField v-model="company" label="Company" placeholder="Pixinvent" />
      </VCol>

      <!-- ember me -->
      <VCol cols="12">
        <VCheckbox v-model="checkbox" label="Remember me" />
      </VCol>

      <VCol cols="12" class="d-flex gap-4">
        <VBtn type="submit"> Submit </VBtn>

        <VBtn type="reset" color="secondary" variant="outlined"> Reset </VBtn>
      </VCol>
    </VRow>
  </VForm>
</template>
