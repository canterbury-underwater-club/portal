using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public class User : EntityDefaults
{
    [MaxLength(128)] public required string FirebaseUserId { get; init; }

    [MaxLength(256)] public required string FirstName { get; set; }

    [MaxLength(256)] public string? LastName { get; set; }

    [MaxLength(254)] public required string EmailAddress { get; set; }

    [MaxLength(32)] public string? HomePhone { get; set; }

    [MaxLength(32)] public string? MobilePhone { get; set; }

    [MaxLength(2048)] public string? PhotoUrl { get; set; }

    public required MembershipStatus MembershipStatus { get; set; } = MembershipStatus.NonMember;

    public DateTime? MembershipStartDate { get; set; }

    public DateTime? MembershipEndDate { get; set; }

    public DateTime LastSignedIn { get; set; }

    public virtual ICollection<Role> Roles { get; init; } = [];
}