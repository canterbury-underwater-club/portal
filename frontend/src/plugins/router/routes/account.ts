import type { RouteRecordRaw } from 'vue-router'
import { Routes } from '../constants'

export const accountRoutes: RouteRecordRaw = {
  path: '/',
  component: () => import('@/layouts/default.vue'),
  meta: { requiresAuth: true },
  children: [
    {
      ...Routes.AccountSettings,
      component: () => import('@/pages/account-settings.vue'),
    },
  ],
}
