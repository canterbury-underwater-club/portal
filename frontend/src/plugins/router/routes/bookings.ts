import { UserRoles } from '@/constants/roles'
import type { RouteRecordRaw } from 'vue-router'
import { Routes } from '../constants'

export const bookingsRoutes: RouteRecordRaw[] = [
  // // PUBLIC
  // {
  //   path: '/bookings/public',
  //   component: () => import('@/layouts/blank.vue'),
  //   children: [
  //     {
  //       path: 'availability',
  //       name: 'bookings-public-availability',
  //       component: () => import('@/pages/bookings/public/Availability.vue'),
  //     },
  //     {
  //       path: 'bookings/:id',
  //       name: 'bookings-public-view',
  //       component: () => import('@/pages/bookings/public/BookingDetailsPublic.vue'),
  //     },
  //   ],
  // },

  // // MINE (authenticated)
  // {
  //   path: '/bookings/mine',
  //   component: () => import('@/layouts/default.vue'),
  //   meta: { requiresAuth: true },
  //   children: [
  //     {
  //       path: '',
  //       name: 'bookings-mine-list',
  //       component: () => import('@/pages/bookings/mine/list.vue'),
  //     },
  //     {
  //       path: 'create',
  //       name: 'bookings-mine-create',
  //       component: () => import('@/pages/bookings/mine/Create.vue'),
  //     },
  //     {
  //       path: ':id',
  //       name: 'bookings-mine-view',
  //       component: () => import('@/pages/bookings/mine/View.vue'),
  //     },
  //     {
  //       path: ':id/edit',
  //       name: 'bookings-mine-edit',
  //       component: () => import('@/pages/bookings/mine/Edit.vue'),
  //     },
  //   ],
  // },

  // ADMIN (committee/booking-admin only)
  {
    ...Routes.BookingsAdmin,
    component: () => import('@/layouts/default.vue'),
    meta: { requiredRoles: [UserRoles.Committee, UserRoles.BookingAdmin] },
    children: [
      { path: '', redirect: { name: Routes.BookingsAdminBookingsList.name } },
      {
        ...Routes.BookingsAdminBookingsList,
        component: () => import('@/pages/bookings/admin/bookings/list.vue'),
      },
      {
        ...Routes.BookingsAdminBookingsCreate,
        component: () => import('@/pages/bookings/admin/bookings/create.vue'),
      },
      //     {
      //       path: 'bookings/:id',
      //       name: 'bookings-admin-view',
      //       component: () => import('@/pages/bookings/admin/bookings/View.vue'),
      //     },
      //     {
      //       path: 'bookings/:id/edit',
      //       name: 'bookings-admin-edit',
      //       component: () => import('@/pages/bookings/admin/bookings/Edit.vue'),
      //     },

      //     // rate plans
      //     {
      //       path: 'rate-plans',
      //       name: 'bookings-admin-rateplans-list',
      //       component: () => import('@/pages/bookings/admin/rate-plans/list.vue'),
      //     },
      //     {
      //       path: 'rate-plans/create',
      //       name: 'bookings-admin-rateplans-create',
      //       component: () => import('@/pages/bookings/admin/rate-plans/Create.vue'),
      //     },
      //     {
      //       path: 'rate-plans/:id/edit',
      //       name: 'bookings-admin-rateplans-edit',
      //       component: () => import('@/pages/bookings/admin/rate-plans/Edit.vue'),
      //     },

      //     // contract holders
      //     {
      //       path: 'contract-holders',
      //       name: 'bookings-admin-contractholders-list',
      //       component: () => import('@/pages/bookings/admin/contract-holders/list.vue'),
      //     },
      //     {
      //       path: 'contract-holders/create',
      //       name: 'bookings-admin-contractholders-create',
      //       component: () => import('@/pages/bookings/admin/contract-holders/Create.vue'),
      //     },
      //     {
      //       path: 'contract-holders/:id/edit',
      //       name: 'bookings-admin-contractholders-edit',
      //       component: () => import('@/pages/bookings/admin/contract-holders/Edit.vue'),
      //     },
    ],
  },

  // // KEYS (read-only: key distributors + committee/admin)
  // {
  //   path: '/bookings/keys',
  //   component: () => import('@/layouts/default.vue'),
  //   meta: { requiredRoles: [UserRoles.KeyDistributor, UserRoles.Committee, UserRoles.Admin] },
  //   children: [
  //     {
  //       path: 'schedule',
  //       name: 'bookings-keys-schedule',
  //       component: () => import('@/pages/bookings/keys/Schedule.vue'),
  //     },
  //     {
  //       path: 'bookings/:id',
  //       name: 'bookings-keys-view',
  //       component: () => import('@/pages/bookings/keys/BookingView.vue'),
  //     },
  //   ],
  // },
]
