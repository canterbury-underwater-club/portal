using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.ErrorHandling;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Users.Update;

public class Handler(PortalDbContext db, IMapper mapper)
    : IRequestEndpointHandler<Contracts.HandlerRequest, Results<ProblemHttpResult, Ok>>
{
    public async Task<Results<ProblemHttpResult, Ok>> HandleAsync(Contracts.HandlerRequest request,
        CancellationToken cancellationToken = default)
    {
        var user = await db.Users.FindAsync([request.Id], cancellationToken);

        if (user == null) return ProblemTypedResults.NotFound<User>();

        if (request.FirstName is not null) user.FirstName = request.FirstName;
        if (request.LastName is not null) user.LastName = request.LastName;
        if (request.EmailAddress is not null) user.EmailAddress = request.EmailAddress;
        if (request.HomePhone is not null) user.HomePhone = request.HomePhone;
        if (request.MobilePhone is not null) user.MobilePhone = request.MobilePhone;
        if (request.PhotoUrl is not null) user.PhotoUrl = request.PhotoUrl;
        if (request.Address is not null) user.Address = request.Address;
        if (request.DateOfBirth is not null) user.DateOfBirth = request.DateOfBirth;
        if (request.Occupation is not null) user.Occupation = request.Occupation;
        if (request.EmergencyContactName is not null) user.EmergencyContactName = request.EmergencyContactName;
        if (request.EmergencyContactPhone is not null) user.EmergencyContactPhone = request.EmergencyContactPhone;
        if (request.MembershipStatus is not null)
            user.MembershipStatus = mapper.Map<MembershipStatus>(request.MembershipStatus);
        if (request.MembershipStartDate is not null) user.MembershipStartDate = request.MembershipStartDate;
        if (request.MembershipEndDate is not null) user.MembershipEndDate = request.MembershipEndDate;

        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok();
    }
}