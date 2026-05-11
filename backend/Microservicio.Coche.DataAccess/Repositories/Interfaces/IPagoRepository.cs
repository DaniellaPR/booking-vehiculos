using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IPagoRepository
    {
        Task<PagoEntity?> ObtenerPorIdAsync(Guid pagId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PagoEntity>> ObtenerPorReservaAsync(Guid resId, CancellationToken cancellationToken = default);
        Task<PagoEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<PagoEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(PagoEntity pago, CancellationToken cancellationToken = default);
        void Actualizar(PagoEntity pago);
        void Eliminar(PagoEntity pago);
    }
}