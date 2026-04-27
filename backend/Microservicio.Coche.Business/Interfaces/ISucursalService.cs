
using Microservicios.Coche.Business.DTOs.Sucursal;

namespace Microservicios.Coche.Business.Interfaces;

public interface ISucursalService
{
    Task<IReadOnlyList<SucursalResponse>> ListarAsync(CancellationToken cancellationToken = default);
    Task<SucursalResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<SucursalResponse> CrearAsync(CrearSucursalRequest request, CancellationToken cancellationToken = default);
    Task<SucursalResponse> ActualizarAsync(ActualizarSucursalRequest request, CancellationToken cancellationToken = default);

    // 🚨 ESTA ES LA LÍNEA QUE FALTABA
    Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default);
}