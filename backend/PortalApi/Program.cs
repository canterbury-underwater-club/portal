using System.Reflection;
using System.Text.Json.Serialization;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.Services;
using CanterburyUnderwater.PortalApi.WebAppExtensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

        builder.Services
            .AddFirebaseAuthentication()
            .AddAuthorization()
            .AddCorsPolicy(builder.Environment).AddApiVersioning(options => options.ReportApiVersions = true);
        builder.Services
            .AddOpenApiWithBearerSecurity()
            .AddAutoMapper(options => options.AddMaps(assembly))
            .AddEndpoints(assembly)
            .AddEndpointsApiExplorer()
            .AddDefaultApiVersioning()
            .AddProblemDetails()
            .AddHttpContextAccessor();

        builder.Services.ConfigureHttpJsonOptions(options =>
        {
            options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        builder.Services.AddDbContext<PortalDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"));
        });

        builder.Services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
        builder.Services.AddScoped<IClaimsTransformation, UserRolesClaimsTransformation>();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
        }
        else
        {
            app.UseExceptionHandler();
        }

        app.UseStatusCodePages()
            .UseAuthorization()
            .UseCors();

        app.AddEndpointWithDefaultApiVersioning();

        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<PortalDbContext>().Database.Migrate();
        }

        app.Run();
    }
}