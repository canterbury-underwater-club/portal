﻿using CanterburyUnderwater.PortalApi.Features.Users.Models;

namespace CanterburyUnderwater.PortalApi.Features.Users.Update;

public record Contracts
{
    public record Request
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }
        public string? EmailAddress { get; init; }
        public string? HomePhone { get; init; }
        public string? MobilePhone { get; init; }
        public string? PhotoUrl { get; init; }
        public string? Address { get; init; }
        public DateTime? DateOfBirth { get; init; }
        public string? Occupation { get; init; }
        public string? EmergencyContactName { get; init; }
        public string? EmergencyContactPhone { get; init; }
        public MembershipStatusModel? MembershipStatus { get; init; }
        public DateTime? MembershipStartDate { get; init; }
        public DateTime? MembershipEndDate { get; init; }
    }

    public record HandlerRequest : Request
    {
        public required Guid Id { get; init; }
    }
}