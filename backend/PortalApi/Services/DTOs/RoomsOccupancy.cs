namespace CanterburyUnderwater.PortalApi.Services.DTOs;

public record RoomsOccupancy
{
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public required IReadOnlyCollection<DailyRoomsOccupancy> Days { get; init; }
}

public record DailyRoomsOccupancy(DateOnly Date, RoomOccupancy[] Rooms);

public record RoomOccupancy(int Room, RoomOccupancyStatus Status);

public enum RoomOccupancyStatus
{
    Booked,
    Pending
}