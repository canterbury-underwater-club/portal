using CanterburyUnderwater.PortalApi.Features.Users.Models;

namespace CanterburyUnderwater.PortalApi.Features.Users.List;

public record Contracts
{
    public record Response
    {
        public required List<UserModel> Users { get; set; }
    }
}