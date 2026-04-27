using Microservicios.Coche.Business.DTOs.Rol;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class RolService : IRolService
{
    private readonly IRolDataService _rolDataService;

    public RolService(IRolDataService rolDataService)
    {
        _rolDataService = rolDataService;
    }

    public async Task<RolResponse> CrearAsync(CrearRolRequest request, CancellationToken cancellationToken = default)
    {
        var errors = RolValidator.ValidarCreacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de creación de rol es inválida.", errors);

        var dataModel = RolBusinessMapper.ToDataModel(request);
        var creado = await _rolDataService.CrearAsync(dataModel, cancellationToken);

        return RolBusinessMapper.ToResponse(creado);
    }

    public async Task<RolResponse> ActualizarAsync(ActualizarRolRequest request, CancellationToken cancellationToken = default)
    {
        var errors = RolValidator.ValidarActualizacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de rol es inválida.", errors);

        var existente = await _rolDataService.ObtenerPorIdAsync(request.ROL_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró el rol solicitado.");

        var dataModel = RolBusinessMapper.ToDataModel(request);

        // Mantener campos de creación
        dataModel.ROL_fechaCreacion = existente.ROL_fechaCreacion;
        dataModel.ROL_usuarioCreacion = existente.ROL_usuarioCreacion;

        var actualizado = await _rolDataService.ActualizarAsync(dataModel, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el rol porque no existe.");

        return RolBusinessMapper.ToResponse(actualizado);
    }

    public async Task<RolResponse> ObtenerPorIdAsync(Guid rolId, CancellationToken cancellationToken = default)
    {
        var rol = await _rolDataService.ObtenerPorIdAsync(rolId, cancellationToken);

        if (rol is null)
            throw new NotFoundException("No se encontró el rol solicitado.");

        return RolBusinessMapper.ToResponse(rol);
    }

    public async Task<IReadOnlyList<RolResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var roles = await _rolDataService.ListarAsync(cancellationToken);

        return roles.Select(RolBusinessMapper.ToResponse).ToList();
    }
}