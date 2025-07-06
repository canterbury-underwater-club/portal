using CanterburyUnderwater.PortalApi.Features.Members.Models;

namespace CanterburyUnderwater.PortalApi.Features.Members.List;

public record Contracts
{
    public record Response
    {
        public required List<Member> Members { get; set; }
    }
}