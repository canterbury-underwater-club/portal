using AutoMapper;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.Features.Users.Models;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CanterburyUnderwater.PortalApi.Features.Users.SignIn;

public class Handler(PortalDbContext db, ICurrentUserAccessor currentUserAccessor, IMapper mapper)
    : IResponseEndpointHandler<Results<UnauthorizedHttpResult, Ok<Contracts.Response>>>
{
    public async Task<Results<UnauthorizedHttpResult, Ok<Contracts.Response>>> HandleAsync(
        CancellationToken cancellationToken = default)
    {
        var user = await currentUserAccessor.GetCurrentUserAsync(cancellationToken);
        if (user == null) return TypedResults.Unauthorized();

        user.LastSignedIn = DateTime.UtcNow;
        await db.SaveChangesAsync(cancellationToken);

        return TypedResults.Ok(new Contracts.Response
        {
            User = mapper.Map<UserModel>(user)
        });
    }
}