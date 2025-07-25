﻿namespace CanterburyUnderwater.PortalApi.Features.Users.Models;

public record UserModel
{
    public required Guid Id { get; init; }
    public required string FirstName { get; init; }
    public string? LastName { get; init; }
    public required string EmailAddress { get; init; }
    public string? HomePhone { get; init; }
    public string? MobilePhone { get; init; }
    public string? PhotoUrl { get; init; }
    public string? Address { get; init; }
    public DateTime? DateOfBirth { get; init; }
    public string? Occupation { get; init; }
    public string? EmergencyContactName { get; init; }
    public string? EmergencyContactPhone { get; init; }
    public required MembershipStatusModel MembershipStatus { get; init; }
    public DateTime? MembershipStartDate { get; init; }
    public DateTime? MembershipEndDate { get; init; }
    public required ICollection<string> Roles { get; init; }
}