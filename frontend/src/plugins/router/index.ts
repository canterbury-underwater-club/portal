import { useRouteLoadingStore } from '@/stores/routeLoading'
import type { App } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import { authGuard } from './guards/authGuard'
import { routes } from './routes'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes,
})

router.beforeEach(() => useRouteLoadingStore().start())

router.beforeEach(authGuard)

router.afterEach(() => useRouteLoadingStore().stop())

export default function (app: App) {
  app.use(router)
}

export { router }
