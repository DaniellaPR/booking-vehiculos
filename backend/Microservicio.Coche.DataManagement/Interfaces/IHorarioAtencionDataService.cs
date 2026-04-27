using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IHorarioAtencionDataService
{
    Task<HorarioAtencionDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<HorarioAtencionDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<HorarioAtencionDataModel>> ListarPorSucursalAsync(Guid sucursalId, CancellationToken cancellationToken = default);
    Task<DataPagedResult<HorarioAtencionDataModel>> BuscarAsync(HorarioAtencionFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<HorarioAtencionDataModel> CrearAsync(HorarioAtencionDataModel model, CancellationToken cancellationToken = default);
    Task<HorarioAtencionDataModel?> ActualizarAsync(HorarioAtencionDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}