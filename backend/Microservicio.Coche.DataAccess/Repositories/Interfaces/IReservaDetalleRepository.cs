using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IReservaDetalleRepository
    {
        Task<ReservaDetalleEntity?> ObtenerPorIdAsync(Guid rexId, CancellationToken cancellationToken = default);
        Task<ReservaDetalleEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ReservaDetalleEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(ReservaDetalleEntity detalle, CancellationToken cancellationToken = default);
        void Actualizar(ReservaDetalleEntity detalle);
        void Eliminar(ReservaDetalleEntity detalle);
    }
}
