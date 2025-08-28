using AutoMapper;
using AutoMapper.QueryableExtensions;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Get;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Ok<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var booking = await db.Bookings
            .AsNoTracking()
            .Where(b => b.Id == request.BookingId)
            .ProjectTo<BookingModel>(mapper.ConfigurationProvider)
            .SingleOrDefaultAsync(ct);

        if (booking == null) return ProblemTypedResults.NotFound<BookingModel>(request.BookingId);

        return TypedResults.Ok(new Contracts.Response { Booking = booking });
    }
}