
using Microservicios.Coche.Business.DTOs.UsuarioApp;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class UsuarioAppService : IUsuarioAppService
{
    private readonly IUsuarioAppDataService _usuarioDataService;
    private readonly IRolDataService _rolDataService; // Inyectado para validar la existencia del Rol

    public UsuarioAppService(
        IUsuarioAppDataService usuarioDataService,
        IRolDataService rolDataService)
    {
        _usuarioDataService = usuarioDataService;
        _rolDataService = rolDataService;
    }

    public async Task<UsuarioAppResponse> CrearAsync(CrearUsuarioAppRequest request, CancellationToken cancellationToken = default)
    {
        var errors = UsuarioAppValidator.ValidarCreacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de creación de usuario es inválida.", errors);

        // Validar regla de negocio: El rol debe existir
        var rolExistente = await _rolDataService.ObtenerPorIdAsync(request.ROL_id, cancellationToken);
        if (rolExistente is null)
            throw new NotFoundException("El rol asociado no existe.");

        // Opcional/Recomendado: Validar que el email sea único llamando a un método en _usuarioDataService

        var dataModel = UsuarioAppBusinessMapper.ToDataModel(request);
        var creado = await _usuarioDataService.CrearAsync(dataModel, cancellationToken);

        return UsuarioAppBusinessMapper.ToResponse(creado);
    }

    public async Task<UsuarioAppResponse> ActualizarAsync(ActualizarUsuarioAppRequest request, CancellationToken cancellationToken = default)
    {
        var errors = UsuarioAppValidator.ValidarActualizacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de usuario es inválida.", errors);

        var existente = await _usuarioDataService.ObtenerPorIdAsync(request.USU_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró el usuario solicitado.");

        // Validar regla de negocio: El rol debe existir si es que se cambió
        if (existente.ROL_id != request.ROL_id)
        {
            var rolExistente = await _rolDataService.ObtenerPorIdAsync(request.ROL_id, cancellationToken);
            if (rolExistente is null)
                throw new NotFoundException("El nuevo rol asociado no existe.");
        }

        var dataModel = UsuarioAppBusinessMapper.ToDataModel(request);

        // Conservar campos de auditoría de creación
        dataModel.USU_fechaCreacion = existente.USU_fechaCreacion;
        dataModel.USU_usuarioCreacion = existente.USU_usuarioCreacion;

        var actualizado = await _usuarioDataService.ActualizarAsync(dataModel, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el usuario porque no existe.");

        return UsuarioAppBusinessMapper.ToResponse(actualizado);
    }

    public async Task<UsuarioAppResponse> ObtenerPorIdAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var usuario = await _usuarioDataService.ObtenerPorIdAsync(usuarioId, cancellationToken);

        if (usuario is null)
            throw new NotFoundException("No se encontró el usuario solicitado.");

        return UsuarioAppBusinessMapper.ToResponse(usuario);
    }

    public async Task<IReadOnlyList<UsuarioAppResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var usuarios = await _usuarioDataService.ListarAsync(cancellationToken);

        return usuarios.Select(UsuarioAppBusinessMapper.ToResponse).ToList();
    }
    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existente = await _usuarioDataService.ObtenerPorIdAsync(id, cancellationToken);
        if (existente is null)
            throw new NotFoundException($"No se encontró el usuario con ID {id}.");
        return await _usuarioDataService.EliminarAsync(id, cancellationToken);
    }
}