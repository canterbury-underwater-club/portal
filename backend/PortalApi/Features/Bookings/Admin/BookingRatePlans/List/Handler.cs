using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.BookingRatePlans.List;

public class Handler(PortalDbContext db, IMapper mapper)
    : IResponseEndpointHandler<Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(CancellationToken ct = default)
    {
        var ratePlans = await db.BookingRatePlans.ToListAsync(ct);

        var response = new Contracts.Response
        {
            RatePlans = mapper.Map<List<BookingRatePlanModel>>(ratePlans)
        };

        return TypedResults.Ok(response);
    }
}