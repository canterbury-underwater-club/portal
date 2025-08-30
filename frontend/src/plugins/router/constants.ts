export interface RouteDef {
  name: string
  path: string
}

export const Routes = {
  Error: { name: 'error', path: '/:pathMatch(.*)*' },
  Login: { name: 'login', path: '/login' },
  LoginCallback: { name: 'login-callback', path: '/login-callback' },
  AccountSettings: { name: 'account-settings', path: '/account-settings' },

  Dashboard: { name: 'dashboard', path: '/dashboard' },

  PeopleList: { name: 'people-list', path: '/people' },
  PeopleView: { name: 'people-view', path: '/people/:id' },
  PeopleEdit: { name: 'people-edit', path: '/people/:id/edit' },
} as const satisfies Record<string, RouteDef>
