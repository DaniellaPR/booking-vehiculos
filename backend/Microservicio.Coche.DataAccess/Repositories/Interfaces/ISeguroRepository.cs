using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface ISeguroRepository
    {
        Task<SeguroEntity?> ObtenerPorIdAsync(Guid segId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<SeguroEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(SeguroEntity seguro, CancellationToken cancellationToken = default);
        void Actualizar(SeguroEntity seguro);
        void Eliminar(SeguroEntity seguro);
        Task<SeguroEntity> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken);
    }
}