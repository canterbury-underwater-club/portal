namespace CanterburyUnderwater.PortalApi.Features.Users.Models;

public record UserModel
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string EmailAddress { get; init; }
    public string? HomePhone { get; init; }
    public string? MobilePhone { get; init; }
    public required MemberStatusModel MembershipStatus { get; init; }
    public DateTime? MembershipStartDate { get; init; }
    public DateTime? MembershipEndDate { get; init; }
}