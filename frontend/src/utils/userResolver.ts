import { MemberStatusModel, UserModel } from '@/api/generated/v1'

export function resolveUserPhoto(user: UserModel | null | undefined) {
  return user?.photoUrl ?? ''
}

export function resolveUserDisplayName(user: UserModel | null | undefined) {
  return `${user?.firstName ?? ''} ${user?.lastName}`
}

export function resolveUserMembershipStatus(
  user: UserModel | null | undefined,
): string | undefined {
  if (!user) return

  switch (user.membershipStatus) {
    case MemberStatusModel.NonMember:
      return 'Non-member'
    case MemberStatusModel.PendingApproval:
      return 'Pending approval'
    default:
      return user.membershipStatus
  }
}
