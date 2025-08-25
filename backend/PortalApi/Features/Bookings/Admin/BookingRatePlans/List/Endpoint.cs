using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/admin/rate-plans",
                async Task<IResult> (IResponseEndpointHandler<Ok<Contracts.Response>> handler, CancellationToken ct) =>
                    await handler.HandleAsync(ct))
            .Produces<Contracts.Response>()
            .WithTags(Tags.BookingsAdminRatePlans)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}