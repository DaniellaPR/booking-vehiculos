using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IAuditoriaDataService
{
    Task<AuditoriaDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<AuditoriaDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<AuditoriaDataModel>> BuscarAsync(AuditoriaFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<AuditoriaDataModel> CrearAsync(AuditoriaDataModel model, CancellationToken cancellationToken = default);
}
