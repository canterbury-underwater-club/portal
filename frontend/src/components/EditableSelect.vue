<script setup lang="ts">
const model = defineModel<unknown | undefined>()
defineProps<{
  editing: boolean
}>()
</script>

<template>
  <VSelect
    v-model="model"
    :variant="editing ? undefined : 'plain'"
    :readonly="!editing"
    :persistent-hint="!editing"
    :compact="!editing"
    v-bind="$attrs"
    :class="{ 'hide-expand-icon': !editing }"
  />
</template>

<style scoped>
.hide-expand-icon :deep(.v-select__menu-icon) {
  display: none !important;
}
</style>
