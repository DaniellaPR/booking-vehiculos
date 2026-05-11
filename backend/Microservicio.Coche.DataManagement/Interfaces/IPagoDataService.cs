using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IPagoDataService
{
    Task<PagoDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PagoDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<PagoDataModel> CrearAsync(PagoDataModel model, CancellationToken cancellationToken = default);
    Task<PagoDataModel?> ActualizarAsync(PagoDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
    Task<DataPagedResult<PagoDataModel>> BuscarAsync(PagoFiltroDataModel filtro, CancellationToken cancellationToken = default);
}