using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IMantenimientoRepository
    {
        Task<MantenimientoEntity?> ObtenerPorIdAsync(Guid manId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MantenimientoEntity>> ObtenerPorVehiculoAsync(Guid vehiculoId, CancellationToken cancellationToken = default);
        Task<MantenimientoEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<MantenimientoEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(MantenimientoEntity mantenimiento, CancellationToken cancellationToken = default);
        void Actualizar(MantenimientoEntity mantenimiento);
        void Eliminar(MantenimientoEntity mantenimiento);
    }
}
