using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Services;

public interface IBookingService
{
    Task<BookingConflicts> GetConflictsAsync(Booking booking,
        CancellationToken ct = default);

    Task<BookingRatePlan> GetCurrentRatePlanAsync(CancellationToken ct = default);

    Task<RoomsOccupancy> GetOccupancyAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken ct = default);
}

public class BookingService(PortalDbContext db) : IBookingService
{
    public async Task<BookingConflicts> GetConflictsAsync(Booking booking,
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

        return new BookingConflicts(conflicts);
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

    public async Task<RoomsOccupancy> GetOccupancyAsync(
        DateOnly from,
        DateOnly to,
        CancellationToken ct = default)
    {
        if (to < from) throw new ArgumentException("'to' must be on/after 'from'.");

        var overlapping = await db.Bookings
            .AsNoTracking()
            .Where(b => b.BookingStatus != BookingStatus.Cancelled)
            .Where(b => b.CheckInDate <= to && b.CheckOutDate >= from)
            .Select(b => new { b.CheckInDate, b.CheckOutDate, b.Rooms, b.BookingStatus })
            .ToListAsync(ct);

        var perDay = new Dictionary<DateOnly, Dictionary<int, RoomOccupancyStatus>>();

        foreach (var b in overlapping)
        {
            // Clamp to request dates
            var start = b.CheckInDate < from ? from : b.CheckInDate;
            var end = b.CheckOutDate > to ? to : b.CheckOutDate;

            var status = b.BookingStatus == BookingStatus.Approved
                ? RoomOccupancyStatus.Booked
                : RoomOccupancyStatus.Pending;

            for (var date = start; date <= end; date = date.AddDays(1))
            {
                if (!perDay.TryGetValue(date, out var roomsForDay))
                    perDay[date] = roomsForDay = new Dictionary<int, RoomOccupancyStatus>();

                foreach (var room in b.Rooms)
                    if (roomsForDay.TryGetValue(room, out var existing))
                    {
                        // Precedence: Booked > Pending
                        if (existing == RoomOccupancyStatus.Pending && status == RoomOccupancyStatus.Booked)
                            roomsForDay[room] = RoomOccupancyStatus.Booked;
                    }
                    else
                    {
                        roomsForDay[room] = status;
                    }
            }
        }

        var days = perDay
            .OrderBy(kv => kv.Key)
            .Select(kv =>
            {
                var states = kv.Value
                    .OrderBy(r => r.Key)
                    .Select(r => new RoomOccupancy(r.Key, r.Value))
                    .ToArray();

                return new DailyRoomsOccupancy(kv.Key, states);
            })
            .ToList();

        return new RoomsOccupancy
        {
            From = from,
            To = to,
            Days = days
        };
    }
}