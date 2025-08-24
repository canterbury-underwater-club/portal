using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingContractHolders.Create;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost(
                "bookings/admin/contract-holders",
                async Task<IResult> (
                        Contracts.Request request,
                        IRequestEndpointHandler<Contracts.Request,
                            Results<ProblemHttpResult, Created<Contracts.Response>>> handler,
                        CancellationToken ct) =>
                    await handler.HandleAsync(request, ct))
            .Produces<Contracts.Response>(StatusCodes.Status201Created)
            .WithTags(Tags.BookingsAdminContractHolders)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}