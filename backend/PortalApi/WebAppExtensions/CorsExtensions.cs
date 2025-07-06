namespace CanterburyUnderwater.PortalApi.WebAppExtensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicy(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                if (env.IsDevelopment())
                    policy.AllowAnyOrigin();
                else
                    policy.WithOrigins("https://portal.canterburyunderwater.org.nz");
            });
        });

        return services;
    }
}