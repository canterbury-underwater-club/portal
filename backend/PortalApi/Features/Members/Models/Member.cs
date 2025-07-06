namespace CanterburyUnderwater.PortalApi.Features.Members.Models;

public record Member
{
    public required Guid Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}