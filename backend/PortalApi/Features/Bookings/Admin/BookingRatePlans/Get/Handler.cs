using AutoMapper;
using AutoMapper.QueryableExtensions;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Get;

public class Handler(PortalDbContext db, IBookingService bookingService, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Ok<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var ratePlan = await db.BookingRatePlans
            .AsNoTracking()
            .Where(rp => rp.Id == request.RatePlanId)
            .ProjectTo<BookingRatePlanModel>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(ct);

        if (ratePlan == null) return ProblemTypedResults.NotFound<BookingRatePlanModel>(request.RatePlanId);

        try
        {
            var current = await bookingService.GetCurrentRatePlanAsync(ct);
            ratePlan.IsCurrent = ratePlan.Id == current.Id;
        }
        catch
        {
            // Ignore if there is no current rate plan
        }

        return TypedResults.Ok(new Contracts.Response { RatePlan = ratePlan });
    }
}