using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;

namespace CanterburyUnderwater.Endpoints;

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