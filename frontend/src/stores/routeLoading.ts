import { defineStore } from 'pinia'
import { ref } from 'vue'

const INITIAL_DELAY_MS = 500
const MINIMUM_SHOWN_MS = 300

export const useRouteLoadingStore = defineStore('routeLoading', () => {
  const loading = ref(false)

  let showTimeout: ReturnType<typeof setTimeout> | null = null
  let hideTimeout: ReturnType<typeof setTimeout> | null = null
  let loaderShownAt = 0

  function start() {
    // Cancel any previous hiding
    if (hideTimeout) clearTimeout(hideTimeout)
    hideTimeout = null

    // If already scheduled to show, don't reschedule
    if (showTimeout) return

    showTimeout = setTimeout(() => {
      loading.value = true
      loaderShownAt = Date.now()
      showTimeout = null
    }, INITIAL_DELAY_MS)
  }

  function stop() {
    // If loader hasn't shown yet, cancel the scheduled show
    if (showTimeout) {
      clearTimeout(showTimeout)
      showTimeout = null
      return
    }
    // If loader is showing, ensure it remains for at least MINIMUM_SHOWN_MS
    const elapsed = Date.now() - loaderShownAt
    if (elapsed < MINIMUM_SHOWN_MS) {
      hideTimeout = setTimeout(() => {
        loading.value = false
        hideTimeout = null
      }, MINIMUM_SHOWN_MS - elapsed)
    } else {
      loading.value = false
    }
  }

  return { loading, start, stop }
})
