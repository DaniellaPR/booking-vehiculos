using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IAuditoriaRepository
    {
        Task<AuditoriaEntity?> ObtenerPorIdAsync(Guid audId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<AuditoriaEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(AuditoriaEntity auditoria, CancellationToken cancellationToken = default);
    }
}
