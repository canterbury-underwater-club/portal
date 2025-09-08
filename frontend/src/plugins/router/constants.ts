export interface RouteDef {
  name: string
  path: string
}

const BaseRoutePaths = {
  BookingsAdmin: '/bookings/admin',
} as const

export const Routes = {
  Error: { name: 'error', path: '/:pathMatch(.*)*' },
  Login: { name: 'login', path: '/login' },
  LoginCallback: { name: 'login-callback', path: '/login-callback' },
  AccountSettings: { name: 'account-settings', path: '/account-settings' },

  Dashboard: { name: 'dashboard', path: '/dashboard' },

  PeopleList: { name: 'people-list', path: '/people' },
  PeopleCreate: { name: 'people-create', path: '/people/create' },
  PeopleView: { name: 'people-view', path: '/people/:id' },
  PeopleEdit: { name: 'people-edit', path: '/people/:id/edit' },

  BookingsAdmin: { name: 'bookings-admin', path: BaseRoutePaths.BookingsAdmin },
  BookingsAdminBookingsList: {
    name: 'bookings-admin-bookings-list',
    path: `${BaseRoutePaths.BookingsAdmin}/bookings`,
  },
  BookingsAdminBookingsCreate: {
    name: 'bookings-admin-bookings-create',
    path: `${BaseRoutePaths.BookingsAdmin}/bookings/create`,
  },
} as const satisfies Record<string, RouteDef>
