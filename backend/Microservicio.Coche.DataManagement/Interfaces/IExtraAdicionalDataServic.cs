using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IExtraAdicionalDataService
{
    Task<ExtraAdicionalDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExtraAdicionalDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<ExtraAdicionalDataModel>> BuscarAsync(ExtraAdicionalFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<ExtraAdicionalDataModel> CrearAsync(ExtraAdicionalDataModel model, CancellationToken cancellationToken = default);
    Task<ExtraAdicionalDataModel?> ActualizarAsync(ExtraAdicionalDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}