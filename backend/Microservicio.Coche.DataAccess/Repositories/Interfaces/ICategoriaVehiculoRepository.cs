using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface ICategoriaVehiculoRepository
    {
        Task<CategoriaVehiculoEntity?> ObtenerPorIdAsync(Guid catId, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<CategoriaVehiculoEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(CategoriaVehiculoEntity categoria, CancellationToken cancellationToken = default);
        void Actualizar(CategoriaVehiculoEntity categoria);
        void Eliminar(CategoriaVehiculoEntity categoria);
        Task<CategoriaVehiculoEntity> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken);
    }
}