using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<Booking, BookingModel>();
    }
}