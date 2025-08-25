using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.List;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet(
                "bookings/admin/contract-holders",
                async Task<IResult> (IResponseEndpointHandler<Ok<Contracts.Response>> handler, CancellationToken ct) =>
                    await handler.HandleAsync(ct))
            .Produces<Contracts.Response>()
            .WithTags(Tags.BookingsAdminContractHolders)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}