using AutoMapper;
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
            .OrderByDescending(rp => rp.EffectiveFrom)
            .ToListAsync(ct);

        var current = await bookingService.GetCurrentRatePlanAsync(ct);

        var models = mapper.Map<List<BookingRatePlanModel>>(ratePlans);

        foreach (var m in models)
            m.IsCurrent = m.Id == current.Id;

        var response = new Contracts.Response { RatePlans = models };
        return TypedResults.Ok(response);
    }
}