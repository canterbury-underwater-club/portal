import { useCurrentUserStore } from '@/stores'
import { waitForState } from '@/utils/waitForState'
import { NavigationGuard } from 'vue-router'
import { Routes } from '../constants'

export const authGuard: NavigationGuard = async (to, from, next) => {
  const recordWithAuth = to.matched.find((r) => r.meta?.requiresAuth || r.meta?.requiredRoles)
  const requiredRoles = recordWithAuth?.meta?.requiredRoles as string | string[] | undefined
  const requiresAuth = Boolean(recordWithAuth?.meta?.requiresAuth || requiredRoles)

  if (!requiresAuth) return next()

  const currentUserStore = useCurrentUserStore()
  await waitForState(() => !currentUserStore.loading)

  const user = currentUserStore.user
  if (!user) {
    return next({ name: Routes.Login.name, query: { redirect: to.fullPath } })
  }

  if (requiredRoles) {
    const roles = Array.isArray(requiredRoles) ? requiredRoles : [requiredRoles]
    const hasRole = user.roles?.some((r) => roles.includes(r))
    if (!hasRole) return next({ name: Routes.Error.name })
  }

  next()
}
