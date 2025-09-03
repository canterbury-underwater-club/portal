using AutoMapper;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Public.Occupancy;

public class Handler(IBookingService bookingService, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request, Ok<Contracts.Response>>
{
    public async Task<Ok<Contracts.Response>> HandleAsync(Contracts.Request request, CancellationToken ct = default)
    {
        var occupany = await bookingService.GetOccupancyAsync(request.From, request.To, ct);

        return TypedResults.Ok(new Contracts.Response
        {
            Occupancy = mapper.Map<RoomsOccupancyModel>(occupany)
        });
    }
}