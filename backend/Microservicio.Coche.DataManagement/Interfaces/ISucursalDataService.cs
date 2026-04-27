using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface ISucursalDataService
{
    Task<IReadOnlyList<SucursalDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<SucursalDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SucursalDataModel> CrearAsync(SucursalDataModel model, CancellationToken cancellationToken = default);
    Task<SucursalDataModel?> ActualizarAsync(SucursalDataModel model, CancellationToken cancellationToken = default);
    Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default);
}