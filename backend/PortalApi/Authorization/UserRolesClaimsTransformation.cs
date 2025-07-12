using System.Security.Claims;
using CanterburyUnderwater.PortalApi.Services;
using Microsoft.AspNetCore.Authentication;

namespace CanterburyUnderwater.PortalApi.Authorization;

public class UserRolesClaimsTransformation(ICurrentUserAccessor currentUserAccessor)
    : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = (ClaimsIdentity)principal.Identity!;

        if (identity.Claims.All(c => c.Type != identity.RoleClaimType))
        {
            var user = await currentUserAccessor.GetCurrentUserAsync(identity);
            if (user?.Roles != null)
                foreach (var role in user.Roles)
                    identity.AddClaim(new Claim(identity.RoleClaimType, role.Name));
        }

        return principal;
    }
}