using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.Get;

public class Handler(PortalDbContext db, IBookingService bookingService, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Ok<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var ratePlan = await db.BookingRatePlans.FindAsync([request.RatePlanId], ct);

        if (ratePlan == null) return ProblemTypedResults.NotFound<BookingRatePlanModel>(request.RatePlanId);

        var model = mapper.Map<BookingRatePlanModel>(ratePlan);

        try
        {
            var current = await bookingService.GetCurrentRatePlanAsync(ct);
            model.IsCurrent = model.Id == current.Id;
        }
        catch
        {
            // Ignore if there is no current rate plan
        }

        var response = new Contracts.Response
        {
            RatePlan = model
        };

        return TypedResults.Ok(response);
    }
}