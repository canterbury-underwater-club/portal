using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.CreateBooking;

public class Handler(PortalDbContext db, IBookingService bookingService, IMapper mapper)
    : IRequestEndpointHandler<Contracts.Request,
        Results<ProblemHttpResult, Created<Contracts.Response>>>
{
    public async Task<Results<ProblemHttpResult, Created<Contracts.Response>>> HandleAsync(Contracts.Request request,
        CancellationToken ct = default)
    {
        var primaryContactExists = await db.Users
            .AsNoTracking()
            .AnyAsync(u => u.Id == request.PrimaryContactId, ct);
        if (!primaryContactExists)
            return ProblemTypedResults.NotFound($"{nameof(Contracts.Request.PrimaryContactId)} not found.");

        if (request.ContractHolderId != null)
        {
            var holderExists = await db.BookingContractHolders
                .AsNoTracking()
                .AnyAsync(h => h.Id == request.ContractHolderId && h.IsActive, ct);
            if (!holderExists)
                return ProblemTypedResults.NotFound($"{nameof(Contracts.Request.ContractHolderId)} not found.");
        }

        var ratePlan = await bookingService.GetCurrentRatePlanAsync(ct);

        var booking = mapper.Map<Booking>(request);
        booking.BookingRatePlanId = ratePlan.Id;

        // Validate availability
        // Create booking
        return ProblemTypedResults.NotFound<User>();
    }
}