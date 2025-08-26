using AutoMapper;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;

namespace CanterburyUnderwater.PortalApi.Features.Bookings.Admin.Bookings.Update;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Contracts.Request, Contracts.HandlerRequest>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<Contracts.HandlerRequest, Booking>()
            .ForMember(d => d.PrimaryContact, o => o.Ignore())
            .ForMember(d => d.ContractHolder, o => o.Ignore())
            .ForMember(d => d.BookingRatePlan, o => o.Ignore())
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember, ctx) => !IsNullOrDefault(srcMember)));
    }

    private static bool IsNullOrDefault(object? value)
    {
        if (value is null) return true;

        // When a nullable<T> has a value, AutoMapper passes the boxed underlying T (not Nullable<T>)
        var t = value.GetType();

        // Strings: treat empty/whitespace as "default" for PATCH (optional, keep if you want to allow empty strings)
        if (t == typeof(string))
            return string.IsNullOrWhiteSpace((string)value);

        // For value types: compare with default(T)
        if (t.IsValueType)
        {
            var defaultValue = Activator.CreateInstance(t)!; // default(T)
            return value.Equals(defaultValue);
        }

        // Collections: treat empty as a real value; the .Condition above only skips null.
        // If you want to also skip empty collections, add:
        // if (value is System.Collections.IEnumerable e && !e.Cast<object?>().Any()) return true;

        return false;
    }
}