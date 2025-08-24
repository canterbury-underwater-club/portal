namespace CanterburyUnderwater.Endpoints;

public interface IRequestEndpointHandler<in TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct = default);
}

public interface IRequestEndpointHandler<in TRequest>
{
    Task HandleAsync(TRequest request, CancellationToken ct = default);
}

public interface IResponseEndpointHandler<TResponse>
{
    Task<TResponse> HandleAsync(CancellationToken ct = default);
}