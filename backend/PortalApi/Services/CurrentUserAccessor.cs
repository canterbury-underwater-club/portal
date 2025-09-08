using System.Security.Claims;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi.Services;

public interface ICurrentUserAccessor
{
    Task<User?> GetCurrentUserAsync(CancellationToken ct = default);
    Task<User?> GetCurrentUserAsync(ClaimsIdentity? identity, CancellationToken ct = default);
}

public class CurrentUserAccessor(IHttpContextAccessor httpContextAccessor, PortalDbContext db) : ICurrentUserAccessor
{
    private User? _cachedUser;

    public Task<User?> GetCurrentUserAsync(CancellationToken ct = default)
    {
        return GetCurrentUserAsync(null, ct);
    }

    public async Task<User?> GetCurrentUserAsync(ClaimsIdentity? identity,
        CancellationToken ct = default)
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
                u => u.FirebaseUserId == firebaseUser.UserId || u.EmailAddress == firebaseUser.EmailAddress ||
                     u.SecondaryEmailAddress == firebaseUser.EmailAddress,
                ct);

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

            await db.Users.AddAsync(user, ct);
        }
        else
        {
            user.FirstName = firebaseUser.UserId;
            user.FirstName = firebaseUser.FirstName;
            user.LastName = firebaseUser.LastName ?? user.LastName;
            user.PhotoUrl = firebaseUser.PhotoUrl ?? user.PhotoUrl;

            if (user.SecondaryEmailAddress != firebaseUser.EmailAddress) user.EmailAddress = firebaseUser.EmailAddress;
        }

        await db.SaveChangesAsync(ct);

        _cachedUser = user;
        return user;
    }
}