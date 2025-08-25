using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Get;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Ok<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var ratePlan = await db.BookingRatePlans.FindAsync([request.RatePlanId], ct);

        if (ratePlan == null) return ProblemTypedResults.NotFound<BookingRatePlanModel>(request.RatePlanId);

        var response = new Contracts.Response
        {
            RatePlan = mapper.Map<BookingRatePlanModel>(ratePlan)
        };

        return TypedResults.Ok(response);
    }
}