import 'vue-router'

declare module 'vue-router' {
  interface RouteMeta {
    requiredRoles?: string | string[]
  }
}
