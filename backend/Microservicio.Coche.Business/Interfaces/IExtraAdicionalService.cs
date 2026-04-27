using Microservicios.Coche.Business.DTOs.ExtraAdicional;

namespace Microservicios.Coche.Business.Interfaces
{
    public interface IExtraAdicionalService
    {
        Task<IReadOnlyList<ExtraAdicionalResponse>> ListarAsync(CancellationToken cancellationToken);
        Task<ExtraAdicionalResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<ExtraAdicionalResponse> CrearAsync(CrearExtraAdicionalRequest request, CancellationToken cancellationToken);
        Task<ExtraAdicionalResponse> ActualizarAsync(ActualizarExtraAdicionalRequest request, CancellationToken cancellationToken);
        Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken);
    }
}