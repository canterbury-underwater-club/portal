using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Models;

public class EntityToModelMappingProfile : Profile
{
    public EntityToModelMappingProfile()
    {
        CreateMap<BookingRateType, BookingRateTypeModel>();
        CreateMap<BookingFeeBasis, BookingFeeBasisModel>();
        CreateMap<BookingStatus, BookingStatusModel>();
        CreateMap<BookingBondStatus, BookingBondStatusModel>();
        CreateMap<BookingAttendeeType, BookingAttendeeTypeModel>();
        CreateMap<BookingAgeBracket, BookingAgeBracketModel>();
        CreateMap<BookingRatePlan, BookingRatePlanModel>();
        CreateMap<BookingRate, BookingRateModel>()
            .ForMember(d => d.AttendeeType, o => o.MapFrom(s =>
                s.AttendeeType == null
                    ? (BookingAttendeeTypeModel?)null
                    : (BookingAttendeeTypeModel)(int)s.AttendeeType.Value))
            .ForMember(d => d.AgeBracket, o => o.MapFrom(s =>
                s.AgeBracket == null
                    ? (BookingAgeBracketModel?)null
                    : (BookingAgeBracketModel)(int)s.AgeBracket.Value));
        CreateMap<BookingFee, BookingFeeModel>();
        CreateMap<BookingAttendee, BookingAttendeeModel>()
            .ForMember(d => d.AttendeeType, o => o.MapFrom(s =>
                s.AttendeeType == null
                    ? (BookingAttendeeTypeModel?)null
                    : (BookingAttendeeTypeModel)(int)s.AttendeeType.Value))
            .ForMember(d => d.AgeBracket, o => o.MapFrom(s =>
                s.AgeBracket == null
                    ? (BookingAgeBracketModel?)null
                    : (BookingAgeBracketModel)(int)s.AgeBracket.Value));
    }
}