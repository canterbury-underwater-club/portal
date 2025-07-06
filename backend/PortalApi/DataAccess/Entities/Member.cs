namespace CanterburyUnderwater.PortalApi.DataAccess.Entities;

public class Member : EntityDefaults
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string EmailAddress { get; set; }
    public required MemberStatus Status { get; set; }
    public DateTime DateJoined { get; set; }
}