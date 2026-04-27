using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IHorarioAtencionRepository
    {
        Task<HorarioAtencionEntity?> ObtenerPorIdAsync(Guid horId, CancellationToken cancellationToken = default);
        Task<HorarioAtencionEntity?> ObtenerParaActualizarAsync(Guid horId, CancellationToken cancellationToken = default);

        // El Listar debe devolver la Entidad, NO un 'object'
        Task<IReadOnlyList<HorarioAtencionEntity>> ListarAsync(CancellationToken cancellationToken = default);

        // (Opcional) Este método lo tienes extra, está perfecto si lo implementaste en tu repositorio
        Task<IReadOnlyList<HorarioAtencionEntity>> ObtenerPorSucursalAsync(Guid sucursalId, CancellationToken cancellationToken = default);

        Task AgregarAsync(HorarioAtencionEntity horario, CancellationToken cancellationToken = default);
        void Actualizar(HorarioAtencionEntity horario);
        void Eliminar(HorarioAtencionEntity horario);
    }
}