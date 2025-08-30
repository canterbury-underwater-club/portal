using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/mine",
                async Task<IResult> (
                        DateOnly? from,
                        DateOnly? to,
                        IRequestEndpointHandler<Contracts.Request, Ok<Contracts.Response>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(
                        new Contracts.Request { From = from, To = to }, ct))
            .Produces<Contracts.Response>()
            .WithTags(Tags.BookingsMine)
            .RequireAuthorization();
    }
}