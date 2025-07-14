<script setup lang="ts">
import { debounce } from 'lodash-es'
import { ref, watch } from 'vue'

interface SuggestionOption {
  title: string
  value: google.maps.places.PlacePrediction | null
}

const model = defineModel<string | null | undefined>()
defineProps<{
  editing: boolean
}>()

const loading = ref(false)
const searchToken = ref<google.maps.places.AutocompleteSessionToken | undefined>(undefined)

const suggestionOptions = ref<SuggestionOption[]>([])

let autocompleteLib: google.maps.PlacesLibrary | null = null

async function ensurePlacesLib() {
  if (!autocompleteLib)
    autocompleteLib = (await google.maps.importLibrary('places')) as google.maps.PlacesLibrary
  return autocompleteLib
}

async function startSearchSession() {
  const lib = await ensurePlacesLib()
  if (!searchToken.value) {
    searchToken.value = new lib.AutocompleteSessionToken()
  }
}

const getSuggestions = debounce(async (search: string | null | undefined) => {
  if (!search || search.length < 3) {
    suggestionOptions.value = []
    loading.value = false
    return
  }

  loading.value = true
  await startSearchSession()
  const lib = await ensurePlacesLib()
  const { AutocompleteSuggestion } = lib

  const request: google.maps.places.AutocompleteRequest = {
    input: search,
    language: 'en-US',
    region: 'nz',
    sessionToken: searchToken.value,
  }

  const { suggestions: suggestionArr } =
    await AutocompleteSuggestion.fetchAutocompleteSuggestions(request)

  suggestionOptions.value = suggestionArr.map((suggestion) => ({
    title: suggestion.placePrediction?.text?.toString() ?? '',
    value: suggestion.placePrediction,
  }))

  loading.value = false
}, 200)

async function onAddressSelected(placePrediction: google.maps.places.PlacePrediction | null) {
  if (!placePrediction) return

  const place = placePrediction.toPlace()
  await place.fetchFields({ fields: ['formattedAddress'] })

  if (place.formattedAddress) model.value = place.formattedAddress
}

// Watch address changes
watch(model, async (val) => {
  await getSuggestions(val)
})
</script>

<template>
  <VCombobox
    v-model="model"
    :items="suggestionOptions"
    label="Address"
    :loading="editing && loading"
    hide-no-data
    :variant="editing ? undefined : 'plain'"
    :readonly="!editing"
    :persistent-hint="!editing"
    :compact="!editing"
    validate-on="input"
    :class="{ 'hide-expand-icon': !editing }"
    v-bind="$attrs"
  >
    <template #item="{ item, props }">
      <VListItem v-bind="props" @click="onAddressSelected(item.value)" />
    </template>
  </VCombobox>
</template>

<style scoped>
.hide-expand-icon :deep(.v-combobox__menu-icon) {
  display: none !important;
}
</style>
