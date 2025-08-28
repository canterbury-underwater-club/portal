using AutoMapper;
using AutoMapper.QueryableExtensions;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.List;

public class Handler(PortalDbContext db, IBookingService bookingService, IMapper mapper)
    : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken ct = default)
    {
        var ratePlans = await db.BookingRatePlans
            .AsNoTracking()
            .OrderByDescending(rp => rp.EffectiveFrom)
            .ProjectTo<BookingRatePlanModel>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

        var current = await bookingService.GetCurrentRatePlanAsync(ct);

        foreach (var plan in ratePlans)
            plan.IsCurrent = plan.Id == current.Id;

        var response = new Contracts.Response { RatePlans = ratePlans };
        return TypedResults.Ok(response);
    }
}