using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IExtraAdicionalRepository
    {
        Task<ExtraAdicionalEntity?> ObtenerPorIdAsync(Guid extId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<ExtraAdicionalEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(ExtraAdicionalEntity extra, CancellationToken cancellationToken = default);
        void Actualizar(ExtraAdicionalEntity extra);
        void Eliminar(ExtraAdicionalEntity extra);
        Task<ExtraAdicionalEntity> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken);
    }
}