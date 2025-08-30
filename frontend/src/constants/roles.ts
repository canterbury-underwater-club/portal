export const UserRoles = {
  Admin: 'Admin',
  Committee: 'Committee',
  BookingAdmin: 'BookingAdmin',
  KeyDistributor: 'KeyDistributor',
} as const

export type UserRole = (typeof UserRoles)[keyof typeof UserRoles]
