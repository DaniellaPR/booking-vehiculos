using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.Business.DTOs.Sucursal;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Mappers;

namespace Microservicios.Coche.Business.Services;

public class SucursalService : ISucursalService
{
    private readonly ISucursalDataService _sucursalDataService;

    public SucursalService(ISucursalDataService sucursalDataService)
    {
        _sucursalDataService = sucursalDataService;
    }

    public async Task<SucursalResponse> CrearAsync(CrearSucursalRequest request, CancellationToken cancellationToken = default)
    {
        var dataModel = request.ToDataModel();
        var creado = await _sucursalDataService.CrearAsync(dataModel, cancellationToken);

        return creado.ToResponse();
    }

    public async Task<SucursalResponse> ActualizarAsync(ActualizarSucursalRequest request, CancellationToken cancellationToken = default)
    {
        var existente = await _sucursalDataService.ObtenerPorIdAsync(request.SucursalId, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró la sucursal solicitada.");

        // Actualizar solo los campos modificables manteniendo la auditoría intacta
        existente.SUC_nombre = request.SUC_nombre;
        existente.SUC_ciudad = request.SUC_ciudad;
        existente.SUC_direccion = request.SUC_direccion;
        existente.SUC_coordenadas = request.SUC_coordenadas;
        existente.SUC_usuarioModificacion = request.ModificadoPorUsuario;

        var actualizada = await _sucursalDataService.ActualizarAsync(existente, cancellationToken);

        if (actualizada is null)
            throw new NotFoundException("No se pudo actualizar la sucursal porque no existe.");

        return actualizada.ToResponse();
    }

    public async Task<SucursalResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var sucursal = await _sucursalDataService.ObtenerPorIdAsync(id, cancellationToken);

        if (sucursal is null)
            throw new NotFoundException("No se encontró la sucursal solicitada.");

        return sucursal.ToResponse();
    }

    public async Task<IReadOnlyList<SucursalResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var sucursales = await _sucursalDataService.ListarAsync(cancellationToken);

        return sucursales.Select(s => s.ToResponse()).ToList();
    }

    public async Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
    {
        var existente = await _sucursalDataService.ObtenerPorIdAsync(id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró la sucursal a eliminar.");

        await _sucursalDataService.EliminarLogicoAsync(id, usuario, cancellationToken);
    }


}