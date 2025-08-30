import type { RouteRecordRaw } from 'vue-router'
import { Routes } from '../constants'

export const authAndErrorRoutes: RouteRecordRaw = {
  path: '/',
  component: () => import('@/layouts/blank.vue'),
  children: [
    { ...Routes.Login, component: () => import('@/pages/login.vue') },
    {
      ...Routes.LoginCallback,
      component: () => import('@/pages/login-callback.vue'),
    },
    {
      ...Routes.Error,
      component: () => import('@/pages/[...error].vue'),
    },
  ],
}
