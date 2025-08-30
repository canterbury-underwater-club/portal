using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Mine.Get;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Results<ProblemHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Ok<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        throw new NotImplementedException();

        // // Add where PrimaryContactId is current user id to query
        // var booking = await db.Bookings
        //     .AsNoTracking()
        //     .Where(b => b.Id == request.BookingId)
        //     .ProjectTo<BookingModel>(mapper.ConfigurationProvider)
        //     .SingleOrDefaultAsync(ct);
        //
        // if (booking == null) return ProblemTypedResults.NotFound<BookingModel>(request.BookingId);
        //
        // return TypedResults.Ok(new Contracts.Response { Booking = booking });
    }
}