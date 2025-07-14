using System.ComponentModel.DataAnnotations;

namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public class User : EntityDefaults
{
    [MaxLength(128)] public string? FirebaseUserId { get; init; }

    [MaxLength(Constants.Validation.MaxNameLength)]
    public required string FirstName { get; set; }

    [MaxLength(Constants.Validation.MaxNameLength)]
    public string? LastName { get; set; }

    [MaxLength(Constants.Validation.MaxEmailAddressLength)]
    public required string EmailAddress { get; set; }

    [MaxLength(Constants.Validation.MaxPhoneLength)]
    public string? HomePhone { get; set; }

    [MaxLength(Constants.Validation.MaxPhoneLength)]
    public string? MobilePhone { get; set; }

    [MaxLength(2048)] public string? PhotoUrl { get; set; }

    [MaxLength(Constants.Validation.MaxAddressLength)]
    public string? Address { get; set; }

    public DateTime? DateOfBirth { get; set; }

    [MaxLength(Constants.Validation.MaxOccupationLength)]
    public string? Occupation { get; set; }

    [MaxLength(Constants.Validation.MaxNameLength * 2)]
    public string? EmergencyContactName { get; set; }

    [MaxLength(Constants.Validation.MaxPhoneLength)]
    public string? EmergencyContactPhone { get; set; }

    public required MembershipStatus MembershipStatus { get; set; } = MembershipStatus.NonMember;

    public DateTime? MembershipStartDate { get; set; }

    public DateTime? MembershipEndDate { get; set; }

    public DateTime? LastSignedIn { get; set; }

    public virtual ICollection<Role> Roles { get; init; } = [];
}