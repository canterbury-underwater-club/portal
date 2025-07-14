<script setup lang="ts">
import { format, isValid, parseISO } from 'date-fns'

const model = defineModel<string | null | undefined>()
const props = defineProps<{
  editing: boolean
}>()

const menu = ref(false)

const displayValue = computed(() => {
  if (!model.value) return ''
  const date = typeof model.value === 'string' ? parseISO(model.value) : model.value
  return isValid(date) ? format(date, 'dd MMMM yyyy') : model.value
})

function selectDate(val: string | null) {
  if (!val) return
  model.value = val
  menu.value = false
}

function handleClick() {
  if (props.editing) {
    menu.value = true
  }
}
</script>

<template>
  <VTextField
    :model-value="displayValue"
    :variant="editing ? undefined : 'plain'"
    readonly
    :persistent-hint="!editing"
    :compact="!editing"
    @click="handleClick"
    v-bind="$attrs"
    :class="{ 'editing-text-field': editing }"
    cursor="pointer"
  >
    <VMenu activator="parent" v-model="menu" :close-on-content-click="false">
      <VDatePicker
        :model-value="model"
        @update:model-value="selectDate"
        color="primary"
        hide-actions
      />
    </VMenu>
  </VTextField>
</template>

<style lang="scss" scoped>
.editing-text-field :deep(*) {
  cursor: pointer;
}
</style>
