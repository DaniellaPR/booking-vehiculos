using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microservicios.Coche.DataManagement.Common;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IUsuarioAppDataService
{
    Task<UsuarioAppDataModel?> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<UsuarioAppDataModel?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<UsuarioAppDataModel>> ListarAsync(CancellationToken cancellationToken = default);
    Task<DataPagedResult<UsuarioAppDataModel>> BuscarAsync(UsuarioAppFiltroDataModel filtro, CancellationToken cancellationToken = default);
    Task<UsuarioAppDataModel> CrearAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default);
    Task<UsuarioAppDataModel?> ActualizarAsync(UsuarioAppDataModel model, CancellationToken cancellationToken = default);
    Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default);
}