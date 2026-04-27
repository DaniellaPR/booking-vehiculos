
using Microservicios.Coche.Business.DTOs.HorarioAtencion;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class HorarioAtencionService : IHorarioAtencionService
{
    private readonly IHorarioAtencionDataService _horarioDataService;
    private readonly ISucursalDataService _sucursalDataService; // Para validar la FK

    public HorarioAtencionService(
        IHorarioAtencionDataService horarioDataService,
        ISucursalDataService sucursalDataService)
    {
        _horarioDataService = horarioDataService;
        _sucursalDataService = sucursalDataService;
    }

    public async Task<HorarioAtencionResponse> CrearAsync(CrearHorarioAtencionRequest request, CancellationToken cancellationToken = default)
    {
        var errors = HorarioAtencionValidator.ValidarCreacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de creación de horario es inválida.", errors);

        // Validar que la Sucursal maestra realmente exista
        var sucursalExistente = await _sucursalDataService.ObtenerPorIdAsync(request.SUC_id, cancellationToken);
        if (sucursalExistente is null)
            throw new NotFoundException("La sucursal asociada no existe.");

        var dataModel = HorarioAtencionBusinessMapper.ToDataModel(request);
        var creado = await _horarioDataService.CrearAsync(dataModel, cancellationToken);

        return HorarioAtencionBusinessMapper.ToResponse(creado);
    }

    public async Task<HorarioAtencionResponse> ActualizarAsync(ActualizarHorarioAtencionRequest request, CancellationToken cancellationToken = default)
    {
        var errors = HorarioAtencionValidator.ValidarActualizacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de horario es inválida.", errors);

        var existente = await _horarioDataService.ObtenerPorIdAsync(request.HOR_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró el horario solicitado.");

        var sucursalExistente = await _sucursalDataService.ObtenerPorIdAsync(request.SUC_id, cancellationToken);
        if (sucursalExistente is null)
            throw new NotFoundException("La sucursal asociada no existe.");

        var dataModel = HorarioAtencionBusinessMapper.ToDataModel(request);

        // Conservar campos de auditoría de creación
        dataModel.HOR_fechaCreacion = existente.HOR_fechaCreacion;
        dataModel.HOR_usuarioCreacion = existente.HOR_usuarioCreacion;

        var actualizado = await _horarioDataService.ActualizarAsync(dataModel, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el horario porque no existe.");

        return HorarioAtencionBusinessMapper.ToResponse(actualizado);
    }

    public async Task<HorarioAtencionResponse> ObtenerPorIdAsync(Guid horarioId, CancellationToken cancellationToken = default)
    {
        var horario = await _horarioDataService.ObtenerPorIdAsync(horarioId, cancellationToken);

        if (horario is null)
            throw new NotFoundException("No se encontró el horario solicitado.");

        return HorarioAtencionBusinessMapper.ToResponse(horario);
    }

    public async Task<IReadOnlyList<HorarioAtencionResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var horarios = await _horarioDataService.ListarAsync(cancellationToken);

        return horarios.Select(HorarioAtencionBusinessMapper.ToResponse).ToList();
    }
}