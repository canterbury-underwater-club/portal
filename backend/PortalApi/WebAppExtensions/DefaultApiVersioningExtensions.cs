using Asp.Versioning;
using Asp.Versioning.Builder;
using CanterburyUnderwater.Endpoints;

namespace CanterburyUnderwater.PortalApi.WebAppExtensions;

public static class DefaultApiVersioningExtensions
{
    public static IServiceCollection AddDefaultApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        }).AddApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        return services;
    }

    public static WebApplication AddEndpointWithDefaultApiVersioning(this WebApplication app)
    {
        var appGroup = app.DefaultMapGroup(app.DefaultApiVersionSet());
        app.MapEndpoints(appGroup);

        return app;
    }

    private static ApiVersionSet DefaultApiVersionSet(this WebApplication app)
    {
        return app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .ReportApiVersions()
            .Build();
    }

    private static RouteGroupBuilder DefaultMapGroup(this WebApplication app, ApiVersionSet apiVersionSet)
    {
        return app.MapGroup("v{version:apiVersion}")
            .WithApiVersionSet(apiVersionSet);
    }
}