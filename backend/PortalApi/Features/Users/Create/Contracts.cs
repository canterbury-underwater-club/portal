using CanterburyUnderwater.PortalApi.Features.Users.Models;

namespace CanterburyUnderwater.PortalApi.Features.Users.Create;

public record Contracts
{
    public record Request : CreateUserModel;

    public record Response
    {
        public required UserModel User { get; set; }
    }
}