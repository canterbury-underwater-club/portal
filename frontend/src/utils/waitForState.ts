import type { WatchStopHandle } from 'vue'
import { watch } from 'vue'

/**
 * Waits for a state getter function to return true.
 * @param stateGetter - A function that returns the state to watch.
 * @param timeout - Maximum time to wait in milliseconds. Default is 5000ms.
 * @returns A promise that resolves when the state becomes true or rejects on timeout.
 */
export const waitForState = (stateGetter: () => boolean, timeout: number = 5000): Promise<void> => {
  return new Promise((resolve, reject) => {
    const startTime = Date.now()
    // eslint-disable-next-line prefer-const
    let stopWatching: WatchStopHandle

    const checkState = () => {
      if (stateGetter()) {
        if (stopWatching) stopWatching()
        resolve()
      } else if (Date.now() - startTime >= timeout) {
        if (stopWatching) stopWatching()
        reject(new Error('State did not become true within the timeout period.'))
      }
    }

    stopWatching = watch(
      stateGetter,
      (newValue) => {
        if (newValue) {
          if (stopWatching) stopWatching()
          resolve()
        } else if (Date.now() - startTime >= timeout) {
          if (stopWatching) stopWatching()
          reject(new Error('State did not become true within the timeout period.'))
        }
      },
      { immediate: true },
    )

    // Initial check
    checkState()
  })
}
