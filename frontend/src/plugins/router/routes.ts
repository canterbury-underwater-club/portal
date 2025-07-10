import { Routes } from './constants'
import { isAuthenticatedGuard } from './guards/isAuthenticatedGuard'

export const routes = [
  { path: '/', redirect: '/dashboard' },
  {
    path: '/',
    component: () => import('@/layouts/default.vue'),
    beforeEnter: isAuthenticatedGuard,
    children: [
      {
        path: Routes.Dashboard,
        name: Routes.Dashboard,
        component: () => import('@/pages/dashboard.vue'),
      },
      {
        path: Routes.AccountSettings,
        name: Routes.AccountSettings,
        component: () => import('@/pages/account-settings.vue'),
      },
      {
        path: 'typography',
        component: () => import('@/pages/typography.vue'),
      },
      {
        path: 'icons',
        component: () => import('@/pages/icons.vue'),
      },
      {
        path: 'cards',
        component: () => import('@/pages/cards.vue'),
      },
    ],
  },
  {
    path: '/',
    component: () => import('@/layouts/blank.vue'),
    children: [
      {
        path: Routes.Login,
        name: Routes.Login,
        component: () => import('@/pages/login.vue'),
      },
      {
        path: Routes.LoginCallback,
        name: Routes.LoginCallback,
        component: () => import('@/pages/login-callback.vue'),
      },
      {
        path: '/:pathMatch(.*)*',
        component: () => import('@/pages/[...error].vue'),
      },
    ],
  },
]
