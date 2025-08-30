import {
  UsersModelsMembershipStatusModel as MembershipStatus,
  UsersModelsUserModel as UserModel,
} from '@/api/generated/v1'

export function resolveUserPhoto(user: UserModel | null | undefined): string {
  if (user?.photoUrl && user.photoUrl.trim() !== '') return user.photoUrl

  if (user?.firstName || user?.lastName) {
    const name = [user.firstName, user.lastName].filter(Boolean).join(' ')
    return `https://ui-avatars.com/api/?name=${encodeURIComponent(name)}&background=50ADA0&color=fff`
  }

  return ''
}

export function resolveUserDisplayName(user: UserModel | null | undefined) {
  return `${user?.firstName ?? ''} ${user?.lastName}`
}

export function resolveUserMembershipStatus(
  user: UserModel | null | undefined,
): string | undefined {
  if (!user) return

  return resolveMembershipStatus(user.membershipStatus)
}

export function resolveUserIsActiveMember(user: UserModel | null | undefined): boolean {
  if (!user) return false

  return new Set<MembershipStatus>([
    MembershipStatus.Associate,
    MembershipStatus.Junior,
    MembershipStatus.Senior,
    MembershipStatus.Couple,
    MembershipStatus.Life,
  ]).has(user.membershipStatus)
}

export function resolveMembershipStatus(membershipStatus: MembershipStatus) {
  switch (membershipStatus) {
    case MembershipStatus.NonMember:
      return 'Non-member'
    case MembershipStatus.PendingApproval:
      return 'Pending approval'
    default:
      return membershipStatus
  }
}
