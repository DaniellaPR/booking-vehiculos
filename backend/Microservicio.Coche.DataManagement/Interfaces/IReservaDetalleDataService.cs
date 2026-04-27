using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IReservaDetalleDataService
{
    Task<ReservaDetalleDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ReservaDetalleDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<ReservaDetalleDataModel>> BuscarAsync(ReservaDetalleFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<ReservaDetalleDataModel> CrearAsync(ReservaDetalleDataModel model, CancellationToken cancellationToken = default);
    Task<ReservaDetalleDataModel?> ActualizarAsync(ReservaDetalleDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}
