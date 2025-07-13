import { useCurrentUserStore } from '@/stores'
import { waitForState } from '@/utils/waitForState'
import { NavigationGuard } from 'vue-router'
import { Routes } from '../constants'

export const authGuard: NavigationGuard = async (to, from, next) => {
  const requiredRoles = to.matched.find((r) => r.meta.requiredRoles)?.meta.requiredRoles
  if (!requiredRoles) return next()

  const currentUserStore = useCurrentUserStore()
  await waitForState(() => !currentUserStore.loading)
  const user = currentUserStore.user

  if (!user) {
    return next({ name: Routes.Login, query: { redirect: to.fullPath } })
  }

  if (
    (typeof requiredRoles === 'string' && !user.roles?.includes(requiredRoles)) ||
    (Array.isArray(requiredRoles) && !requiredRoles.some((role) => user.roles?.includes(role)))
  ) {
    return next({ name: Routes.Error })
  }

  next()
}
