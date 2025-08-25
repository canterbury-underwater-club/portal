using Microsoft.AspNetCore.Authorization;

namespace CanterburyUnderwater.PortalApi.EndpointHandling;

public static class EndpointConventionBuilderExtensions
{
    public static TBuilder RequireRoles<TBuilder>(this TBuilder builder, params string[] roles)
        where TBuilder : IEndpointConventionBuilder
    {
        var rolesString = string.Join(",", roles);
        var attribute = new AuthorizeAttribute { Roles = rolesString };
        builder.RequireAuthorization(attribute);
        return builder;
    }
}