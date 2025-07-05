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
      <!-- First Name -->
      <VCol cols="12">
        <VRow no-gutters>
          <VCol cols="12" md="3">
            <label for="firstNameHorizontalIcons">First Name</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="firstNameHorizontalIcons"
              v-model="firstName"
              prepend-inner-icon="ri-user-line"
              placeholder="John"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <!-- il -->
      <VCol cols="12">
        <VRow no-gutters>
          <VCol cols="12" md="3">
            <label for="emailHorizontalIcons">Email</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="emailHorizontalIcons"
              v-model="email"
              prepend-inner-icon="ri-mail-line"
              placeholder="johndoe@email.com"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <!-- ile -->
      <VCol cols="12">
        <VRow no-gutters>
          <VCol cols="12" md="3">
            <label for="mobileHorizontalIcons">Mobile</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="mobileHorizontalIcons"
              v-model="mobile"
              type="number"
              prepend-inner-icon="ri-smartphone-line"
              placeholder="+1 123 456 7890"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <!-- sword -->
      <VCol cols="12">
        <VRow no-gutters>
          <VCol cols="12" md="3">
            <label for="passwordHorizontalIcons">Password</label>
          </VCol>

          <VCol cols="12" md="9">
            <VTextField
              id="passwordHorizontalIcons"
              v-model="password"
              prepend-inner-icon="ri-lock-line"
              autocomplete="on"
              type="password"
              placeholder="············"
              persistent-placeholder
            />
          </VCol>
        </VRow>
      </VCol>

      <!-- ckbox -->
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
