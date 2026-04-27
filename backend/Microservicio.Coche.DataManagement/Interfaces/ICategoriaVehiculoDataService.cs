using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface ICategoriaVehiculoDataService
{
    Task<IReadOnlyList<CategoriaVehiculoDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<CategoriaVehiculoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<CategoriaVehiculoDataModel> CrearAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default);
    Task<CategoriaVehiculoDataModel?> ActualizarAsync(CategoriaVehiculoDataModel model, CancellationToken cancellationToken = default);
    Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default);
}