<script lang="ts" setup>
const firstName = ref('')
const email = ref('')
const mobile = ref<number>()
const password = ref<string>()
const checkbox = ref(false)
</script>

<template>
  <VForm @submit.prevent="() => {}">
    <VRow>
      <VCol cols="12">
        <VRow no-gutters>
          <!-- First Name -->
          <VCol cols="12" md="3">
            <label for="firstName">First Name</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="firstName"
              v-model="firstName"
              placeholder="John"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <VCol cols="12">
        <VRow no-gutters>
          <!-- il -->
          <VCol cols="12" md="3">
            <label for="email">Email</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="email"
              v-model="email"
              placeholder="johndoe@email.com"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <VCol cols="12">
        <VRow no-gutters>
          <!-- ile -->
          <VCol cols="12" md="3">
            <label for="mobile">Mobile</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="mobile"
              v-model="mobile"
              type="number"
              placeholder="+1 123 456 7890"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <VCol cols="12">
        <VRow no-gutters>
          <!-- sword -->
          <VCol cols="12" md="3">
            <label for="password">Password</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="password"
              v-model="password"
              autocomplete="on"
              type="password"
              placeholder="············"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <!-- ember me -->
      <VCol cols="12">
        <VRow no-gutters>
          <VCol cols="12" md="3" />
          <VCol cols="12" md="9">
            <VCheckbox v-model="checkbox" label="Remember me" />
          </VCol>
        </VRow>
      </VCol>

      <!-- mit and reset button -->
      <VCol cols="12">
        <VRow no-gutters>
          <VCol cols="12" md="3" />
          <VCol cols="12" md="9">
            <VBtn type="submit" class="me-4"> Submit </VBtn>
            <VBtn color="secondary" variant="outlined" type="reset"> Reset </VBtn>
          </VCol>
        </VRow>
      </VCol>
    </VRow>
  </VForm>
</template>
