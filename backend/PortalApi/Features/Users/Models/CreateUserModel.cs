namespace CanterburyUnderwater.PortalApi.Features.Users.Models;

public record CreateUserModel
{
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public required string EmailAddress { get; init; }
    public string? HomePhone { get; init; }
    public string? MobilePhone { get; init; }
    public string? PhotoUrl { get; init; }
    public string? Address { get; init; }
    public DateOnly? DateOfBirth { get; init; }
    public string? Occupation { get; init; }
    public string? EmergencyContactName { get; init; }
    public string? EmergencyContactPhone { get; init; }
    public required MembershipStatusModel MembershipStatus { get; init; }
    public DateOnly? MembershipStartDate { get; init; }
    public DateOnly? MembershipEndDate { get; init; }
    public int? MembershipNumber { get; init; }
    public required ICollection<string> Roles { get; init; }
}