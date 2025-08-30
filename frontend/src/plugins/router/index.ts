import { useRouteLoadingStore } from '@/stores/routeLoading'
import type { App } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import { authGuard } from './guards/authGuard'
import { accountRoutes } from './routes/account'
import { authAndErrorRoutes } from './routes/authAndError'
import { bookingsRoutes } from './routes/bookings'
import { peopleRoutes } from './routes/people'

const baseRedirect = { path: '/', redirect: '/people' }

export const routes = [
  baseRedirect,
  peopleRoutes,
  accountRoutes,
  ...bookingsRoutes,
  authAndErrorRoutes,
]

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
