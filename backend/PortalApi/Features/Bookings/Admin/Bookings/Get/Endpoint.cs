using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Get;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/admin/bookings/{id:guid}",
                async Task<IResult> (
                        Guid id,
                        IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
                            handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(
                        new Contracts.Request { BookingId = id }, ct))
            .Produces<Contracts.Response>()
            .ProducesNotFoundProblem()
            .WithTags(Tags.BookingsAdminBookings)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}