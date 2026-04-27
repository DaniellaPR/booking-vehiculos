using Microservicios.Coche.Business.DTOs.Auditoria;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class AuditoriaService : IAuditoriaService
{
    private readonly IAuditoriaDataService _auditoriaDataService;

    public AuditoriaService(IAuditoriaDataService auditoriaDataService)
    {
        _auditoriaDataService = auditoriaDataService;
    }

    public async Task<AuditoriaResponse> CrearAsync(CrearAuditoriaRequest request, CancellationToken cancellationToken = default)
    {
        var errors = AuditoriaValidator.ValidarCreacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de creación de auditoría es inválida.", errors);

        var dataModel = AuditoriaBusinessMapper.ToDataModel(request);
        var creado = await _auditoriaDataService.CrearAsync(dataModel, cancellationToken);

        return AuditoriaBusinessMapper.ToResponse(creado);
    }

    public async Task<IReadOnlyList<AuditoriaResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var auditorias = await _auditoriaDataService.ListarAsync(cancellationToken);

        return auditorias.Select(AuditoriaBusinessMapper.ToResponse).ToList();
    }
}