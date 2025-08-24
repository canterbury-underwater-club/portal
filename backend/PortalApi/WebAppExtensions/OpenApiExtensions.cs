using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace CanterburyUnderwater.PortalApi.WebAppExtensions;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiWithBearerSecurity(this IServiceCollection services)
    {
        services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer<BearerSecuritySchemeTransformer>();
            options.AddSchemaTransformer<UniqueSchemaIdsSchemaTransformer>();
        });

        return services;
    }
}

internal sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider)
    : IOpenApiDocumentTransformer
{
    public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context,
        CancellationToken ct)
    {
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();
        if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
        {
            var requirements = new Dictionary<string, OpenApiSecurityScheme>
            {
                ["Bearer"] = new()
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT"
                }
            };
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = requirements;
            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations))
                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecurityScheme { Reference = new OpenApiReference { Id = "Bearer", Type = ReferenceType.SecurityScheme } }] =
                        Array.Empty<string>()
                });
        }
    }
}

internal sealed class UniqueSchemaIdsSchemaTransformer : IOpenApiSchemaTransformer
{
    private const string RootNamespace = "CanterburyUnderwater.PortalApi.Features.";

    public Task TransformAsync(OpenApiSchema schema, OpenApiSchemaTransformerContext context, CancellationToken ct)
    {
        var type = context.JsonTypeInfo.Type;

        if (type.Namespace == null || !type.Namespace.StartsWith(RootNamespace))
            return Task.CompletedTask;

        var schemaId = BuildSchemaId(type);
        schema.Annotations["x-schema-id"] = schemaId;

        return Task.CompletedTask;
    }

    private static string BuildSchemaId(Type type)
    {
        var id = (type.FullName ?? type.Name).Replace("+", "."); // nested types

        if (id.StartsWith(RootNamespace, StringComparison.Ordinal)) id = id[RootNamespace.Length..];
        return id;
    }
}