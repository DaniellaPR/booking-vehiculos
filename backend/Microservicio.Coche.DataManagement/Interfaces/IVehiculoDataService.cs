using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IVehiculoDataService
{
    Task<VehiculoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<VehiculoDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<VehiculoDataModel>> BuscarAsync(VehiculoFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<VehiculoDataModel> CrearAsync(VehiculoDataModel model, CancellationToken cancellationToken = default);
    Task<VehiculoDataModel?> ActualizarAsync(VehiculoDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
