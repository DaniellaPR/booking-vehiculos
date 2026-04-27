using Microservicios.Coche.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories.Interfaces
{
    public interface IClienteRepository
    {
        Task<ClienteEntity?> ObtenerPorIdAsync(Guid cliId, CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerPorCedulaAsync(string cedula, CancellationToken cancellationToken = default);
        Task AgregarAsync(ClienteEntity cliente, CancellationToken cancellationToken = default);
        void Actualizar(ClienteEntity cliente);
        void Eliminar(ClienteEntity cliente);
        Task<IReadOnlyList<ClienteEntity>> ListarAsync(CancellationToken cancellationToken = default);
        Task<ClienteEntity?> ObtenerParaActualizarAsync(Guid cliId, CancellationToken cancellationToken = default);
    }
}
