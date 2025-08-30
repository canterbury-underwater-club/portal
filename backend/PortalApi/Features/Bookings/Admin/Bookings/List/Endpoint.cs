using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/admin/bookings",
                async Task<IResult> (
                        DateOnly? from,
                        DateOnly? to,
                        int? count,
                        IRequestEndpointHandler<Contracts.Request, Ok<Contracts.Response>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(
                        new Contracts.Request { From = from, To = to, Count = count }, ct))
            .Produces<Contracts.Response>()
            .WithTags(Tags.BookingsAdminBookings)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}