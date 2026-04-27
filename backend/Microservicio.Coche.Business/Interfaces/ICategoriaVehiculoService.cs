
using Microservicios.Coche.Business.DTOs.CategoriaVehiculo;

namespace Microservicios.Coche.Business.Interfaces
{
    public interface ICategoriaVehiculoService
    {
        Task<IReadOnlyList<CategoriaVehiculoResponse>> ListarAsync(CancellationToken cancellationToken);
        Task<CategoriaVehiculoResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<CategoriaVehiculoResponse> CrearAsync(CrearCategoriaVehiculoRequest request, CancellationToken cancellationToken);
        Task<CategoriaVehiculoResponse> ActualizarAsync(ActualizarCategoriaVehiculoRequest request, CancellationToken cancellationToken);
        Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken);
    }
}