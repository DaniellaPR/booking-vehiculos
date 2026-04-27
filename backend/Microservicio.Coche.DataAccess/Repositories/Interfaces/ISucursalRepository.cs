using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface ISucursalRepository
    {
        Task<SucursalEntity?> ObtenerPorIdAsync(Guid sucId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<SucursalEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(SucursalEntity sucursal, CancellationToken cancellationToken = default);
        void Actualizar(SucursalEntity sucursal);
        void Eliminar(SucursalEntity sucursal);
        Task<SucursalEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
