namespace CanterburyUnderwater.PortalApi.Features.Users.Models;

public record UserModel : CreateUserModel
{
    public required Guid Id { get; init; }
}