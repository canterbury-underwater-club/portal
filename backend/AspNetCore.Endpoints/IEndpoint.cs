using Microsoft.AspNetCore.Routing;

namespace CanterburyUnderwater.Endpoints;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}