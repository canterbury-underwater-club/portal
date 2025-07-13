import { UserRoles } from '@/constants/roles'
import { Routes } from './constants'

export const routes = [
  { path: '/', redirect: '/dashboard' },
  {
    path: '/',
    component: () => import('@/layouts/default.vue'),
    meta: { requiredRoles: UserRoles.Committee },
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
        redirect: { name: Routes.People },
      },
      {
        path: `${Routes.Person}/:id`,
        name: Routes.Person,
        component: () => import('@/pages/person.vue'),
      },
      {
        path: `${Routes.Person}/:id`,
        name: Routes.Person,
        component: () => import('@/pages/person.vue'),
      },
      {
        path: `${Routes.Person}/:id/edit`,
        name: Routes.PersonEdit,
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
        name: Routes.Error,
        component: () => import('@/pages/[...error].vue'),
      },
    ],
  },
]
