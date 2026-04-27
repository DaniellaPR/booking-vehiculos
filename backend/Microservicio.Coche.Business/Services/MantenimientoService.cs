
using Microservicios.Coche.Business.DTOs.Mantenimiento;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class MantenimientoService : IMantenimientoService
{
    private readonly IMantenimientoDataService _mantenimientoDataService;
    private readonly IVehiculoDataService _vehiculoDataService;

    public MantenimientoService(
        IMantenimientoDataService mantenimientoDataService,
        IVehiculoDataService vehiculoDataService)
    {
        _mantenimientoDataService = mantenimientoDataService;
        _vehiculoDataService = vehiculoDataService;
    }

    public async Task<MantenimientoResponse> CrearAsync(CrearMantenimientoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = MantenimientoValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de mantenimiento es inválida.", errors);

        var vehiculoExistente = await _vehiculoDataService.ObtenerPorIdAsync(request.VEH_id, cancellationToken);
        if (vehiculoExistente is null)
            throw new NotFoundException("El vehículo asociado no existe.");

        var dataModel = MantenimientoBusinessMapper.ToDataModel(request);
        var creado = await _mantenimientoDataService.CrearAsync(dataModel, cancellationToken);

        return MantenimientoBusinessMapper.ToResponse(creado);
    }

    public async Task<MantenimientoResponse> ActualizarAsync(ActualizarMantenimientoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = MantenimientoValidator.ValidarActualizacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de mantenimiento es inválida.", errors);

        var existente = await _mantenimientoDataService.ObtenerPorIdAsync(request.MAN_id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró el mantenimiento solicitado.");

        if (existente.VEH_id != request.VEH_id)
        {
            var vehiculoExistente = await _vehiculoDataService.ObtenerPorIdAsync(request.VEH_id, cancellationToken);
            if (vehiculoExistente is null)
                throw new NotFoundException("El nuevo vehículo asociado no existe.");
        }

        var dataModel = MantenimientoBusinessMapper.ToDataModel(request);

        dataModel.MAN_fechaCreacion = existente.MAN_fechaCreacion;
        dataModel.MAN_usuarioCreacion = existente.MAN_usuarioCreacion;

        var actualizado = await _mantenimientoDataService.ActualizarAsync(dataModel, cancellationToken);
        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el mantenimiento.");

        return MantenimientoBusinessMapper.ToResponse(actualizado);
    }

    public async Task<MantenimientoResponse> ObtenerPorIdAsync(Guid mantenimientoId, CancellationToken cancellationToken = default)
    {
        var mantenimiento = await _mantenimientoDataService.ObtenerPorIdAsync(mantenimientoId, cancellationToken);
        if (mantenimiento is null)
            throw new NotFoundException("No se encontró el mantenimiento solicitado.");

        return MantenimientoBusinessMapper.ToResponse(mantenimiento);
    }

    public async Task<IReadOnlyList<MantenimientoResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var mantenimientos = await _mantenimientoDataService.ListarAsync(cancellationToken);
        return mantenimientos.Select(MantenimientoBusinessMapper.ToResponse).ToList();
    }
}