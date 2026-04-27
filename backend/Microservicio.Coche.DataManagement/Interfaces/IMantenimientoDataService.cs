using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IMantenimientoDataService
{
    Task<MantenimientoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MantenimientoDataModel>> ListarAsync(CancellationToken cancellationToken = default);

    Task<IReadOnlyList<MantenimientoDataModel>> ListarPorVehiculoAsync(Guid vehiculoId, CancellationToken cancellationToken = default);

    Task<DataPagedResult<MantenimientoDataModel>> BuscarAsync(MantenimientoFiltroDataModel filtro, CancellationToken cancellationToken = default);

    Task<MantenimientoDataModel> CrearAsync(MantenimientoDataModel model, CancellationToken cancellationToken = default);

    Task<MantenimientoDataModel?> ActualizarAsync(MantenimientoDataModel model, CancellationToken cancellationToken = default);

    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
