using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IUsuarioAppRepository
    {
        Task<UsuarioAppEntity?> ObtenerPorIdAsync(Guid usuId, CancellationToken cancellationToken = default);
        Task<UsuarioAppEntity?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken = default);
        Task<UsuarioAppEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default);
        Task<IReadOnlyList<UsuarioAppEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task AgregarAsync(UsuarioAppEntity usuario, CancellationToken cancellationToken = default);
        void Actualizar(UsuarioAppEntity usuario);
        void Eliminar(UsuarioAppEntity usuario);
    }
}
