<script setup lang="ts">
const model = defineModel<string | null | undefined>()
const props = defineProps<{
  editing: boolean
  linkType?: 'mailto' | 'tel' | null
}>()

const showLink = computed(() => !props.editing && Boolean(props.linkType))
const link = computed(() => `${props.linkType}:${model.value}`)
const linkRef = ref<HTMLAnchorElement | null>(null)

function handleControlClick() {
  if (showLink && linkRef.value) {
    linkRef.value.click()
  }
}
</script>

<template>
  <div style="position: relative">
    <VTextField
      v-model="model"
      :variant="editing ? undefined : 'plain'"
      :readonly="!editing"
      :persistent-hint="!editing"
      :compact="!editing"
      validate-on="input"
      :class="{ 'hide-input': showLink }"
      v-bind="$attrs"
      @click:control="handleControlClick"
    />

    <a v-if="showLink" :href="link" class="text-primary text-decoration-underline link">
      {{ model }}
    </a>
  </div>
</template>

<style scoped>
.hide-input :deep(input) {
  color: transparent !important;
}

.link {
  position: absolute;
  top: 11px;
  height: 40px;
  display: flex;
  align-items: center;
  z-index: 2;
  background: transparent;
}
</style>
