namespace CanterburyUnderwater.PortalApi.EndpointHandling;

public interface IEndpoint
{
    void MapEndpoint(IEndpointRouteBuilder app);
}