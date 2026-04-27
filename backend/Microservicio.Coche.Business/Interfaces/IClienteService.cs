using Microservicios.Coche.Business.DTOs.Cliente;

namespace Microservicios.Coche.Business.Interfaces;

public interface IClienteService
{
    Task<ClienteResponse> CrearAsync(CrearClienteRequest request, CancellationToken cancellationToken = default);
    Task<ClienteResponse> ActualizarAsync(ActualizarClienteRequest request, CancellationToken cancellationToken = default);
    Task<ClienteResponse> ObtenerPorIdAsync(Guid clienteId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClienteResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}