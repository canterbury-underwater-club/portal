using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.DataAccess.Validators;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Update;

public class Handler(PortalDbContext db, IBookingService bookingService, IMapper mapper)
    : IRequestEndpointHandler<Contracts.HandlerRequest, Results<ProblemHttpResult, ValidationProblem, Ok>>
{
    public async Task<Results<ProblemHttpResult, ValidationProblem, Ok>> HandleAsync(Contracts.HandlerRequest request,
        CancellationToken ct = default)
    {
        var booking = await db.Bookings.FindAsync([request.Id], ct);

        if (booking == null) return ProblemTypedResults.NotFound<BookingModel>(request.Id);

        if (request.PrimaryContactId.HasValue && request.PrimaryContactId.Value != booking.PrimaryContactId)
        {
            var exists = await db.Users.AsNoTracking().AnyAsync(u => u.Id == request.PrimaryContactId.Value, ct);
            if (!exists) return ProblemTypedResults.NotFound<User>($"{nameof(request.PrimaryContactId)} not found.");
        }

        if (request.ContractHolderId.HasValue && request.ContractHolderId.Value != booking.ContractHolderId)
        {
            var ok = await db.BookingContractHolders.AsNoTracking()
                .AnyAsync(h => h.Id == request.ContractHolderId.Value && h.IsActive, ct);
            if (!ok)
                return ProblemTypedResults.NotFound<BookingContractHolder>(
                    $"{nameof(request.ContractHolderId)} not found or inactive.");
        }

        if (request.CheckInDate.HasValue) booking.CheckInDate = request.CheckInDate.Value;
        if (request.CheckOutDate.HasValue) booking.CheckOutDate = request.CheckOutDate.Value;
        if (request.PrimaryContactId.HasValue) booking.PrimaryContactId = request.PrimaryContactId.Value;
        if (request.BookingStatus.HasValue)
            booking.BookingStatus = mapper.Map<BookingStatus>(request.BookingStatus.Value);
        if (request.BondStatus.HasValue) booking.BondStatus = mapper.Map<BookingBondStatus>(request.BondStatus.Value);
        if (request.GroupName.HasValue) booking.GroupName = request.GroupName.Value;
        if (request.Rooms.HasValue) booking.Rooms = request.Rooms.Value is null ? [] : request.Rooms.Value.ToList();
        if (request.ContractHolderId.HasValue) booking.ContractHolderId = request.ContractHolderId.Value;
        if (request.Attendees.HasValue)
            booking.Attendees = request.Attendees.Value is null
                ? []
                : mapper.Map<List<BookingAttendee>>(request.Attendees.Value);

        var validationResult = await new BookingValidator().ValidateAsync(booking, ct);
        if (!validationResult.IsValid) return ProblemTypedResults.Validation(validationResult);

        var conflicts = await bookingService.GetConflictsAsync(booking, ct);
        if (conflicts.HasConflicts)
            return ProblemTypedResults.Conflict("Requested rooms are unavailable for the selected dates.",
                conflicts.Description);

        await db.SaveChangesAsync(ct);

        return TypedResults.Ok();
    }
}