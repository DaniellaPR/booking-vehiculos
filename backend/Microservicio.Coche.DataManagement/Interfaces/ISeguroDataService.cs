using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface ISeguroDataService
{
    Task<IReadOnlyList<SeguroDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<SeguroDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SeguroDataModel> CrearAsync(SeguroDataModel model, CancellationToken cancellationToken = default);
    Task<SeguroDataModel?> ActualizarAsync(SeguroDataModel model, CancellationToken cancellationToken = default);
    Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default);
}