// src/router/routes/people.ts
import { UserRoles } from '@/constants/roles'
import type { RouteRecordRaw } from 'vue-router'
import { Routes } from '../constants'

export const peopleRoutes: RouteRecordRaw = {
  path: '/',
  component: () => import('@/layouts/default.vue'),
  meta: { requiredRoles: UserRoles.Committee },
  children: [
    {
      ...Routes.PeopleList,
      component: () => import('@/pages/people.vue'),
    },
    {
      ...Routes.PeopleView,
      component: () => import('@/pages/person.vue'),
    },
    {
      ...Routes.PeopleEdit,
      component: () => import('@/pages/person.vue'),
    },
  ],
}
