using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface ITarifaRepository
    {
        Task<TarifaEntity?> ObtenerPorIdAsync(Guid tarId, CancellationToken cancellationToken = default);
        Task<TarifaEntity?> ObtenerPorCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default);
        Task<TarifaEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TarifaEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task<IReadOnlyList<TarifaEntity>> ListarPorCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default);
        Task AgregarAsync(TarifaEntity tarifa, CancellationToken cancellationToken = default);
        void Actualizar(TarifaEntity tarifa);
        void Eliminar(TarifaEntity tarifa);
    }
}
