using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface ITarifaDataService
{
    Task<TarifaDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TarifaDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TarifaDataModel>> ListarPorCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default);
    Task<DataPagedResult<TarifaDataModel>> BuscarAsync(TarifaFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<TarifaDataModel> CrearAsync(TarifaDataModel model, CancellationToken cancellationToken = default);
    Task<TarifaDataModel?> ActualizarAsync(TarifaDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
