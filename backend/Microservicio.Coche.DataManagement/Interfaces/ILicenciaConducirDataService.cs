using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface ILicenciaConducirDataService
{
    Task<LicenciaConducirDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<LicenciaConducirDataModel>> ListarPorClienteAsync(Guid clienteId, CancellationToken cancellationToken = default);
    Task<DataPagedResult<LicenciaConducirDataModel>> BuscarAsync(LicenciaConducirFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<LicenciaConducirDataModel> CrearAsync(LicenciaConducirDataModel model, CancellationToken cancellationToken = default);
    Task<LicenciaConducirDataModel?> ActualizarAsync(LicenciaConducirDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<LicenciaConducirDataModel>> ListarAsync(CancellationToken cancellationToken);
}