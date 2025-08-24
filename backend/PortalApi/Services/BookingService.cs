using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Services;

public record BookingConflict
{
    public required DateOnly Date { get; init; }
    public required int Room { get; init; }
}

public interface IBookingService
{
    Task<IReadOnlyCollection<BookingConflict>> GetConflictsAsync(Booking booking,
        CancellationToken ct = default);

    Task<BookingRatePlan> GetCurrentRatePlanAsync(CancellationToken ct = default);
}

public class BookingService(PortalDbContext db) : IBookingService
{
    public async Task<IReadOnlyCollection<BookingConflict>> GetConflictsAsync(Booking booking,
        CancellationToken ct = default)
    {
        var from = booking.CheckInDate;
        var to = booking.CheckOutDate; // inclusive
        var rooms = booking.Rooms;

        // Overlap rule (inclusive): A.Start <= B.End AND A.End >= B.Start
        var overlappingBookings = await db.Bookings
            .AsNoTracking()
            .Where(b => b.BookingStatus != BookingStatus.Cancelled)
            .Where(b => b.Id != booking.Id)
            .Where(b => b.CheckInDate <= to && b.CheckOutDate >= from)
            .Where(b => b.Rooms.Any(r => rooms.Contains(r))) // room intersection
            .ToListAsync(ct);

        var conflicts = new List<BookingConflict>();

        foreach (var overlappingBooking in overlappingBookings)
        {
            var start = from >= overlappingBooking.CheckInDate ? from : overlappingBooking.CheckInDate;
            var end = to <= overlappingBooking.CheckOutDate ? to : overlappingBooking.CheckOutDate; // inclusive

            foreach (var room in overlappingBooking.Rooms.Intersect(rooms))
                for (var date = start; date <= end; date = date.AddDays(1))
                    conflicts.Add(new BookingConflict { Date = date, Room = room });
        }

        return conflicts;
    }

    public async Task<BookingRatePlan> GetCurrentRatePlanAsync(CancellationToken ct = default)
    {
        var currentRatePlan = await db.BookingRatePlans
            .AsNoTracking()
            .OrderByDescending(p => p.EffectiveFrom)
            .FirstOrDefaultAsync(p => p.EffectiveFrom <= DateOnly.FromDateTime(DateTime.UtcNow), ct);

        if (currentRatePlan == null) throw new InvalidOperationException("No current booking rate plan found.");

        return currentRatePlan;
    }
}