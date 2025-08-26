using AutoMapper;
using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Update;

public class Endpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPatch(
                "bookings/admin/bookings/{id:guid}",
                async Task<IResult> (
                    Guid id,
                    Contracts.Request request,
                    IRequestEndpointHandler<Contracts.HandlerRequest, Results<ProblemHttpResult, ValidationProblem, Ok>>
                        handler,
                    IMapper mapper,
                    CancellationToken ct) =>
                {
                    var handlerRequest = mapper.Map<Contracts.HandlerRequest>(request) with { Id = id };
                    return await handler.HandleAsync(handlerRequest, ct);
                })
            .Produces<Ok>()
            .ProducesValidationProblem()
            .ProducesNotFoundProblem()
            .WithTags(Tags.BookingsAdminBookings)
            .RequireRoles(RoleNames.BookingAdmin);
    }
}