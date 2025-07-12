using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Users.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "users",
                async Task<IResult> (
                        IResponseEndpointHandler<Ok<Contracts.Response>> handler,
                        CancellationToken cancellationToken) =>
                    await handler.HandleAsync(cancellationToken))
            .MapToApiVersion(1)
            .Produces<Contracts.Response>()
            .WithTags(Tags.Users)
            .RequireRoles(RoleNames.Committee);
    }
}