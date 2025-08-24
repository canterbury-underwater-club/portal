using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Models;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<BookingRateType, BookingRateTypeModel>();
        CreateMap<BookingFeeBasis, BookingFeeBasisModel>();
        CreateMap<BookingRatePlan, BookingRatePlanModel>();
        CreateMap<BookingRate, BookingRateModel>();
        CreateMap<BookingFee, BookingFeeModel>();
    }
}