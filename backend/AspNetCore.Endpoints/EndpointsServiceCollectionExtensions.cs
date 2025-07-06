using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CanterburyUnderwater.Endpoints;

public static class EndpointsServiceCollectionExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly assembly)
    {
        return services
            .RegisterEndpoints(assembly)
            .RegisterEndpointHandlers(assembly);
    }

    private static IServiceCollection RegisterEndpoints(this IServiceCollection services, Assembly assembly)
    {
        var endpointDescriptors = assembly.DefinedTypes.Where(type =>
                type is { IsAbstract: false, IsInterface: false } && type.IsAssignableTo(typeof(IEndpoint)))
            .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
            .ToArray();

        services.TryAddEnumerable(endpointDescriptors);

        return services;
    }

    private static IServiceCollection RegisterEndpointHandlers(this IServiceCollection services, Assembly assembly)
    {
        var endpointHandlerDescriptors = assembly.DefinedTypes
            .Where(type => type is { IsAbstract: false, IsInterface: false })
            .SelectMany(type => type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                            (
                                i.GetGenericTypeDefinition() == typeof(IRequestEndpointHandler<>) ||
                                i.GetGenericTypeDefinition() == typeof(IRequestEndpointHandler<,>) ||
                                i.GetGenericTypeDefinition() == typeof(IResponseEndpointHandler<>)
                            )
                )
                .Select(i => new { ImplementationType = type, ServiceType = i }))
            .Select(descriptor => ServiceDescriptor.Transient(descriptor.ServiceType, descriptor.ImplementationType))
            .ToArray();

        services.TryAddEnumerable(endpointHandlerDescriptors);

        return services;
    }
}