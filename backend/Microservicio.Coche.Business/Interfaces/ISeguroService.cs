using Microservicios.Coche.Business.DTOs.Seguro;

namespace Microservicios.Coche.Business.Interfaces
{
    public interface ISeguroService
    {
        Task<IReadOnlyList<SeguroResponse>> ListarAsync(CancellationToken cancellationToken);
        Task<SeguroResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken);
        Task<SeguroResponse> CrearAsync(CrearSeguroRequest request, CancellationToken cancellationToken);
        Task<SeguroResponse> ActualizarAsync(ActualizarSeguroRequest request, CancellationToken cancellationToken);
        Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken);
    }
}