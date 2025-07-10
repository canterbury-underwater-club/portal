using CanterburyUnderwater.PortalApi.Features.Users.Models;

namespace CanterburyUnderwater.PortalApi.Features.Users.SignIn;

public record Contracts
{
    public record Response
    {
        public required UserModel User { get; set; }
    }
}