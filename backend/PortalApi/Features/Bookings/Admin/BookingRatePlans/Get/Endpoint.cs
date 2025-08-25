using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Get;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/admin/rate-plans/{id:guid}",
                async Task<IResult> (
                        Guid id,
                        IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
                            handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(new Contracts.Request { RatePlanId = id }, ct))
            .Produces<Contracts.Response>()
            .WithTags(Tags.BookingsAdminRatePlans)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}