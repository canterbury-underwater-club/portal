using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Create;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "bookings/mine",
                async Task<IResult> (
                        Contracts.Request request,
                        IRequestEndpointHandler<Contracts.Request,
                            Results<ProblemHttpResult, ValidationProblem, Created<Contracts.Response>>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(request, ct))
            .Produces<Contracts.Response>(StatusCodes.Status201Created)
            .ProducesValidationProblem()
            .WithTags(Tags.BookingsMine)
            .RequireAuthorization();
    }
}