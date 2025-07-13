export const UserRoles = {
  Admin: 'Admin',
  Committee: 'Committee',
} as const

export type UserRole = (typeof UserRoles)[keyof typeof UserRoles]
