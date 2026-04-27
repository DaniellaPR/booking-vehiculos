using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IReservaRepository
    {
        Task<ReservaEntity?> ObtenerPorIdAsync(Guid resId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReservaEntity>> ObtenerPorClienteAsync(Guid clienteId, CancellationToken cancellationToken = default);
        Task<ReservaEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReservaEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(ReservaEntity reserva, CancellationToken cancellationToken = default);
        void Actualizar(ReservaEntity reserva);
        void Eliminar(ReservaEntity reserva);
    }
}
