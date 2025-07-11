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
        path: Routes.People,
        name: Routes.People,
        component: () => import('@/pages/people.vue'),
      },
      {
        path: Routes.Person,
        redirect: { name: Routes.People }, // Or path: Routes.People
      },
      {
        path: `${Routes.Person}/:id`,
        name: Routes.Person,
        component: () => import('@/pages/person.vue'),
      },
      {
        path: Routes.AccountSettings,
        name: Routes.AccountSettings,
        component: () => import('@/pages/account-settings.vue'),
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
