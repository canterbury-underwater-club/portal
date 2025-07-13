using System.Security.Claims;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Services;

public interface ICurrentUserAccessor
{
    Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default);
    Task<User?> GetCurrentUserAsync(ClaimsIdentity? identity, CancellationToken cancellationToken = default);
}

public class CurrentUserAccessor(IHttpContextAccessor httpContextAccessor, PortalDbContext db) : ICurrentUserAccessor
{
    private User? _cachedUser;

    public Task<User?> GetCurrentUserAsync(CancellationToken cancellationToken = default)
    {
        return GetCurrentUserAsync(null, cancellationToken);
    }

    public async Task<User?> GetCurrentUserAsync(ClaimsIdentity? identity,
        CancellationToken cancellationToken = default)
    {
        if (_cachedUser != null)
            return _cachedUser;

        identity ??= httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

        if (identity is not { IsAuthenticated: true }) return null;

        var firebaseUser = FirebaseUser.FromClaimsIdentity(identity);
        if (firebaseUser == null) return null;

        var user = await db.Users
            .Include(u => u.Roles)
            .SingleOrDefaultAsync(
                u => u.FirebaseUserId == firebaseUser.UserId || u.EmailAddress == firebaseUser.EmailAddress,
                cancellationToken);

        if (user == null)
        {
            user = new User
            {
                FirebaseUserId = firebaseUser.UserId,
                EmailAddress = firebaseUser.EmailAddress,
                FirstName = firebaseUser.FirstName,
                LastName = firebaseUser.LastName,
                MembershipStatus = MembershipStatus.NonMember,
                PhotoUrl = firebaseUser.PhotoUrl
            };

            await db.Users.AddAsync(user, cancellationToken);
        }
        else
        {
            user.FirstName = firebaseUser.UserId;
            user.EmailAddress = firebaseUser.EmailAddress;
            user.FirstName = firebaseUser.FirstName;
            user.LastName = firebaseUser.LastName ?? user.LastName;
            user.PhotoUrl = firebaseUser.PhotoUrl ?? user.PhotoUrl;
        }

        await db.SaveChangesAsync(cancellationToken);

        _cachedUser = user;
        return user;
    }
}