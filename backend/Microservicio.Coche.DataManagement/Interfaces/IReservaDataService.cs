using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IReservaDataService
{
    Task<ReservaDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservaDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<ReservaDataModel>> BuscarAsync(ReservaFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<ReservaDataModel> CrearAsync(ReservaDataModel model, CancellationToken cancellationToken = default);
    Task<ReservaDataModel?> ActualizarAsync(ReservaDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}