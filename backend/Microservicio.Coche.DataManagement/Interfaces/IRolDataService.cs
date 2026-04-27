using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IRolDataService
{
    Task<RolDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<RolDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<RolDataModel>> BuscarAsync(RolFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<RolDataModel> CrearAsync(RolDataModel model, CancellationToken cancellationToken = default);
    Task<RolDataModel?> ActualizarAsync(RolDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
