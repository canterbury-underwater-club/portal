using AutoMapper;
using AutoMapper.QueryableExtensions;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.List;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(Contracts.Request request, CancellationToken ct = default)
    {
        var query = db.Bookings.AsNoTracking().AsQueryable();

        if (request is { From: { } from, To: { } to })
            // overlap with [from, to]
            query = query.Where(b => b.CheckInDate <= to && b.CheckOutDate >= from);
        else if (request.From is { } fromOnly)
            // anything that ends on/after 'from'
            query = query.Where(b => b.CheckOutDate >= fromOnly);
        else if (request.To is { } toOnly)
            // anything that starts on/before 'to'
            query = query.Where(b => b.CheckInDate <= toOnly);

        var bookings = await query
            .AsNoTracking()
            .OrderBy(b => b.CheckInDate)
            .ProjectTo<BookingModel>(mapper.ConfigurationProvider)
            .ToListAsync(ct);

        return TypedResults.Ok(new Contracts.Response { Bookings = bookings });
    }
}