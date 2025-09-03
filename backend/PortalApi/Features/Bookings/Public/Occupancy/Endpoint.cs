using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Public.Occupancy;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/public/occupancy",
                async Task<IResult> (
                        DateOnly from,
                        DateOnly to,
                        IRequestEndpointHandler<Contracts.Request, Ok<Contracts.Response>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(
                        new Contracts.Request { From = from, To = to }, ct))
            .Produces<Contracts.Response>()
            .WithTags(Tags.BookingsPublic)
            .AllowAnonymous();
    }
}