using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Delete;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete(
                "bookings/admin/rate-plans/{id:guid}",
                async Task<IResult> (
                        Guid id,
                        IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, NoContent>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(new Contracts.Request { RatePlanId = id }, ct))
            .Produces(StatusCodes.Status204NoContent)
            .WithTags(Tags.BookingsAdminRatePlans)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}