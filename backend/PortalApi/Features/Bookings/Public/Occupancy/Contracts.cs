namespace CanterburyUnderwater.PortalApi.Features.Bookings.Public.Occupancy;

public record Contracts
{
    public record Request
    {
        public DateOnly From { get; init; }
        public DateOnly To { get; init; }
    }

    public record Response
    {
        public required RoomsOccupancyModel Occupancy { get; set; }
    }
}

public record RoomsOccupancyModel
{
    public required DateOnly From { get; init; }
    public required DateOnly To { get; init; }
    public required List<DailyRoomsOccupancyModel> Days { get; init; }
}

public record DailyRoomsOccupancyModel(DateOnly Date, List<RoomOccupancyModel> Rooms);

public record RoomOccupancyModel(int Room, RoomOccupancyStatusModel Status);

public enum RoomOccupancyStatusModel
{
    Booked,
    Pending
}