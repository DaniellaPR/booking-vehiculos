using Microservicios.Coche.Business.DTOs.LicenciaConducir;

namespace Microservicios.Coche.Business.Interfaces;

public interface ILicenciaConducirService
{
    Task<LicenciaConducirResponse> CrearAsync(CrearLicenciaConducirRequest request, CancellationToken cancellationToken = default);
    Task<LicenciaConducirResponse> ActualizarAsync(ActualizarLicenciaConducirRequest request, CancellationToken cancellationToken = default);
    Task<LicenciaConducirResponse> ObtenerPorIdAsync(Guid licenciaId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LicenciaConducirResponse>> ListarAsync(CancellationToken cancellationToken = default);
}