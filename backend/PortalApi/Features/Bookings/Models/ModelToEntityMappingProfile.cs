using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Models;

public class ModelToEntityMappingProfile : Profile
{
    public ModelToEntityMappingProfile()
    {
        CreateMap<CreateBookingAttendeeModel, BookingAttendee>()
            .ForMember(d => d.BookingId, o => o.Ignore())
            .ForMember(d => d.Booking, o => o.Ignore())
            .ForMember(d => d.LinkedUserId, o => o.Ignore())
            .ForMember(d => d.LinkedUser, o => o.Ignore());

        CreateMap<BookingStatusModel, BookingStatus>();
        CreateMap<BookingBondStatusModel, BookingBondStatus>();
        CreateMap<BookingAttendeeTypeModel, BookingAttendeeType>();
        CreateMap<BookingAgeBracketModel, BookingAgeBracket>();

        CreateMap<BookingRateTypeModel, BookingRateType>();
        CreateMap<BookingFeeBasisModel, BookingFeeBasis>();
        CreateMap<BookingRateModel, BookingRate>();
        CreateMap<BookingFeeModel, BookingFee>();
    }
}