using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.ErrorHandling.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Users.Update;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch(
                "users/{id:guid}",
                async Task<IResult> (
                    Guid id,
                    Contracts.Request request,
                    IRequestEndpointHandler<Contracts.HandlerRequest, Results<ProblemHttpResult, Ok>> handler,
                    IMapper mapper,
                    CancellationToken cancellationToken) =>
                {
                    var handlerRequest = mapper.Map<Contracts.HandlerRequest>(request) with { Id = id };
                    return await handler.HandleAsync(handlerRequest, cancellationToken);
                })
            .MapToApiVersion(1)
            .Produces<Ok>()
            .ProducesNotFoundProblem()
            .WithTags(Tags.Users)
            .RequireAuthorization();
    }
}