using CanterburyUnderwater.Endpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Members.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "members",
                async Task<IResult> (
                        IResponseEndpointHandler<Ok<Contracts.Response>> handler,
                        CancellationToken cancellationToken) =>
                    await handler.HandleAsync(cancellationToken))
            .MapToApiVersion(1)
            .Produces<Contracts.Response>()
            .WithTags(Tags.Members)
            .RequireAuthorization();
    }
}