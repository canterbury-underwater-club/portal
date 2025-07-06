using System.Reflection;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.DataAccess;
using CanterburyUnderwater.PortalApi.WebAppExtensions;
using Microsoft.EntityFrameworkCore;

namespace CanterburyUnderwater.PortalApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var assembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

        builder.Services
            .AddAuthorization()
            .AddCorsPolicy(builder.Environment).AddApiVersioning(options => options.ReportApiVersions = true);
        builder.Services
            .AddOpenApi()
            .AddAutoMapper(options => options.AddMaps(assembly))
            .AddEndpoints(assembly)
            .AddEndpointsApiExplorer()
            .AddDefaultApiVersioning()
            .AddProblemDetails();

        builder.Services.AddDbContext<PortalDbContext>(options =>
        {
            options.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"));
        });


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