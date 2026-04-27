
using Microservicios.coche.Business.Interfaces;
using Microservicios.Coche.Business.DTOs.Vehiculo;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class VehiculoService : IVehiculoService
{
    private readonly IVehiculoDataService _vehiculoDataService;
    private readonly ICategoriaVehiculoDataService _categoriaDataService;
    private readonly ISucursalDataService _sucursalDataService;

    public VehiculoService(
        IVehiculoDataService vehiculoDataService,
        ICategoriaVehiculoDataService categoriaDataService,
        ISucursalDataService sucursalDataService)
    {
        _vehiculoDataService = vehiculoDataService;
        _categoriaDataService = categoriaDataService;
        _sucursalDataService = sucursalDataService;
    }

    public async Task<VehiculoResponse> CrearAsync(CrearVehiculoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = VehiculoValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de vehículo es inválida.", errors);

        // Validaciones cruzadas (Claves Foráneas)
        var categoriaExistente = await _categoriaDataService.ObtenerPorIdAsync(request.CAT_id, cancellationToken);
        if (categoriaExistente is null)
            throw new NotFoundException("La categoría asociada no existe.");

        var sucursalExistente = await _sucursalDataService.ObtenerPorIdAsync(request.SUC_id, cancellationToken);
        if (sucursalExistente is null)
            throw new NotFoundException("La sucursal asociada no existe.");

        var dataModel = VehiculoBusinessMapper.ToDataModel(request);
        var creado = await _vehiculoDataService.CrearAsync(dataModel, cancellationToken);

        return VehiculoBusinessMapper.ToResponse(creado);
    }

    public async Task<VehiculoResponse> ActualizarAsync(ActualizarVehiculoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = VehiculoValidator.ValidarActualizacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de vehículo es inválida.", errors);

        var existente = await _vehiculoDataService.ObtenerPorIdAsync(request.VEH_id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró el vehículo solicitado.");

        if (existente.CAT_id != request.CAT_id)
        {
            var categoriaExistente = await _categoriaDataService.ObtenerPorIdAsync(request.CAT_id, cancellationToken);
            if (categoriaExistente is null)
                throw new NotFoundException("La nueva categoría asociada no existe.");
        }

        if (existente.SUC_id != request.SUC_id)
        {
            var sucursalExistente = await _sucursalDataService.ObtenerPorIdAsync(request.SUC_id, cancellationToken);
            if (sucursalExistente is null)
                throw new NotFoundException("La nueva sucursal asociada no existe.");
        }

        var dataModel = VehiculoBusinessMapper.ToDataModel(request);

        dataModel.VEH_fechaCreacion = existente.VEH_fechaCreacion;
        dataModel.VEH_usuarioCreacion = existente.VEH_usuarioCreacion;

        var actualizado = await _vehiculoDataService.ActualizarAsync(dataModel, cancellationToken);
        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el vehículo.");

        return VehiculoBusinessMapper.ToResponse(actualizado);
    }

    public async Task<VehiculoResponse> ObtenerPorIdAsync(Guid vehiculoId, CancellationToken cancellationToken = default)
    {
        var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(vehiculoId, cancellationToken);
        if (vehiculo is null)
            throw new NotFoundException("No se encontró el vehículo solicitado.");

        return VehiculoBusinessMapper.ToResponse(vehiculo);
    }

    public async Task<IReadOnlyList<VehiculoResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var vehiculos = await _vehiculoDataService.ListarAsync(cancellationToken);
        return vehiculos.Select(VehiculoBusinessMapper.ToResponse).ToList();
    }
}