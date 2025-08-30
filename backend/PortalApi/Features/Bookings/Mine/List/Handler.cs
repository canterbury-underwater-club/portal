using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.List;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(Contracts.Request request, CancellationToken ct = default)
    {
        throw new NotImplementedException();

        // var query = db.Bookings.AsNoTracking().AsQueryable();
        //
        // if (request is { From: { } from, To: { } to })
        //     // overlap with [from, to]
        //     query = query.Where(b => b.CheckInDate <= to && b.CheckOutDate >= from);
        // else if (request.From is { } fromOnly)
        //     // anything that ends on/after 'from'
        //     query = query.Where(b => b.CheckOutDate >= fromOnly);
        // else if (request.To is { } toOnly)
        //     // anything that starts on/before 'to'
        //     query = query.Where(b => b.CheckInDate <= toOnly);
        //
        // // Add where PrimaryContactId is current user id to query
        // var bookings = await query
        //     .AsNoTracking()
        //     .OrderBy(b => b.CheckInDate)
        //     .ProjectTo<BookingModel>(mapper.ConfigurationProvider)
        //     .ToListAsync(ct);
        //
        // return TypedResults.Ok(new Contracts.Response { Bookings = bookings });
    }
}