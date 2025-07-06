import { useAuth } from '@/composables/useAuth'
import { Routes } from '@/plugins/router/constants'
import { NavigationGuard } from 'vue-router'

export const isAuthenticatedGuard: NavigationGuard = async (to, from, next) => {
  const user = await useAuth().getUser()
  if (!(user?.isAnonymous ?? true)) {
    next()
  } else {
    next({ name: Routes.Login, query: { redirect: to.fullPath } })
  }
}
