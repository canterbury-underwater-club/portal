using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Create;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "bookings/admin/rate-plans",
                async Task<IResult> (
                        Contracts.Request request,
                        IRequestEndpointHandler<Contracts.Request,
                            Results<ProblemHttpResult, Created<Contracts.Response>>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(request, ct))
            .Produces<Contracts.Response>(StatusCodes.Status201Created)
            .WithTags(Tags.BookingsAdminRatePlans)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}