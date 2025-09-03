using AutoMapper;
using CanterburyUnderwater.PortalApi.Services.DTOs;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Public.Occupancy;

public class ContractsMappingProfile : Profile
{
    public ContractsMappingProfile()
    {
        CreateMap<RoomsOccupancy, RoomsOccupancyModel>();
        CreateMap<DailyRoomsOccupancy, DailyRoomsOccupancyModel>();
        CreateMap<RoomOccupancy, RoomOccupancyModel>();
        CreateMap<RoomOccupancyStatus, RoomOccupancyStatusModel>();
    }
}