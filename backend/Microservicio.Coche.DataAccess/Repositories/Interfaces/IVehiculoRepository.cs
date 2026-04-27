using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IVehiculoRepository
    {
        Task<VehiculoEntity?> ObtenerPorIdAsync(Guid vehId, CancellationToken cancellationToken = default);
        Task<VehiculoEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<VehiculoEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(VehiculoEntity vehiculo, CancellationToken cancellationToken = default);
        void Actualizar(VehiculoEntity vehiculo);
        void Eliminar(VehiculoEntity vehiculo);
    }
}
