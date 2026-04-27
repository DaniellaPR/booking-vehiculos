using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IClienteDataService
{
    Task<ClienteDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ClienteDataModel?> ObtenerPorCedulaAsync(string cedula, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ClienteDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<ClienteDataModel>> BuscarAsync(ClienteFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<ClienteDataModel> CrearAsync(ClienteDataModel model, CancellationToken cancellationToken = default);
    Task<ClienteDataModel?> ActualizarAsync(ClienteDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}