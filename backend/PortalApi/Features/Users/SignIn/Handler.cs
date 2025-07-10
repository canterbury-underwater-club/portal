using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using CanterburyUnderwater.PortalApi.Features.Users.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Features.Users.SignIn;

public class Handler(PortalDbContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    : IResponseEndpointHandler<Results<UnauthorizedHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<UnauthorizedHttpResult, Ok<Contracts.Response>>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var firebaseUser = FirebaseUserPrincipal.FromClaimsPrincipal(httpContextAccessor.HttpContext?.User);
        if (firebaseUser == null) return TypedResults.Unauthorized();

        var user = await dbContext.Users.SingleOrDefaultAsync(u => u.FirebaseUserId == firebaseUser.UserId,
            cancellationToken);
        if (user == null)
        {
            user = new User
            {
                FirebaseUserId = firebaseUser.UserId,
                EmailAddress = firebaseUser.EmailAddress,
                FirstName = firebaseUser.FirstName,
                LastName = firebaseUser.LastName,
                MembershipStatus = MembershipStatus.NonMember
            };

            await dbContext.Users.AddAsync(user, cancellationToken);
        }

        user.LastSignedIn = DateTime.UtcNow;

        await dbContext.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(new Contracts.Response
        {
            User = mapper.Map<UserModel>(user)
        });
    }
}