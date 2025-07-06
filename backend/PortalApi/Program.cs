using System.Reflection;
using CanterburyUnderwater.Endpoints;
using CanterburyUnderwater.PortalApi.WebAppExtensions;

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

        app.Run();
    }
}