using CanterburyUnderwater.Endpoints;
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
                        CancellationToken cancellationToken) =>
                    await handler.HandleAsync(cancellationToken))
            .MapToApiVersion(1)
            .Produces<Contracts.Response>()
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}