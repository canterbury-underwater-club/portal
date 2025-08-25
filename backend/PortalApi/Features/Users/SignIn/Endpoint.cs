using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Users.SignIn;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "users/sign-in",
                async Task<IResult> (
                        IResponseEndpointHandler<Results<UnauthorizedHttpResult, Ok<Contracts.Response>>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(ct))
            .MapToApiVersion(1)
            .Produces<Contracts.Response>()
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}