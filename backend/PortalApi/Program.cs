using System.Reflection;
using System.Text.Json.Serialization;
using CanterburyUnderwater.PortalApi.Authorization;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.EndpointHandling;
using CanterburyUnderwater.PortalApi.ErrorHandling;
using CanterburyUnderwater.PortalApi.Services;
using CanterburyUnderwater.PortalApi.WebAppExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;

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
            .AddCorsPolicy(builder.Environment)
            .AddApiVersioning(options => options.ReportApiVersions = true);
        builder.Services
            .AddOpenApiWithBearerSecurity()
            .AddAutoMapper(options => options.AddMaps(assembly))
            .AddEndpoints(assembly)
            .AddEndpointsApiExplorer()
            .AddDefaultApiVersioning()
            .AddProblemDetails()
            .AddValidatorsFromAssembly(assembly)
            .AddFluentValidationAutoValidation()
            .AddHttpContextAccessor()
            .AddExceptionHandler<PostgreSqlExceptionHandler>();

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
        builder.Services.AddScoped<IBookingService, BookingService>();

        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "v1"));
        }

        app.UseExceptionHandler()
            .UseStatusCodePages()
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