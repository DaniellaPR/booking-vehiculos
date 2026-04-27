using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<RolEntity?> ObtenerPorIdAsync(Guid rolId, CancellationToken cancellationToken = default);
        Task<RolEntity?> ObtenerParaActualizarAsync(Guid rolId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<RolEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(RolEntity rol, CancellationToken cancellationToken = default);
        void Actualizar(RolEntity rol);
        void Eliminar(RolEntity rol);
    }
}
