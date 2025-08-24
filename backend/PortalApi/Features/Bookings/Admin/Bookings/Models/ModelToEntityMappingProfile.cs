using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Models;

public class ModelToEntityMappingProfile : Profile
{
    public ModelToEntityMappingProfile()
    {
        CreateMap<CreateBookingModel, Booking>()
            .ForMember(d => d.BookingRatePlanId, o => o.Ignore())
            .ForMember(d => d.BookingRatePlan, o => o.Ignore())
            .ForMember(d => d.PrimaryContact, o => o.Ignore())
            .ForMember(d => d.ContractHolder, o => o.Ignore());
    }
}