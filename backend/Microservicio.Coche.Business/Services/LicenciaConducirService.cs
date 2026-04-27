
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.DTOs.LicenciaConducir;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class LicenciaConducirService : ILicenciaConducirService
{
    private readonly ILicenciaConducirDataService _licenciaDataService;
    private readonly IClienteDataService _clienteDataService; // Inyectado para validar existencia del cliente

    public LicenciaConducirService(
        ILicenciaConducirDataService licenciaDataService,
        IClienteDataService clienteDataService)
    {
        _licenciaDataService = licenciaDataService;
        _clienteDataService = clienteDataService;
    }

    public async Task<LicenciaConducirResponse> CrearAsync(CrearLicenciaConducirRequest request, CancellationToken cancellationToken = default)
    {
        var errors = LicenciaConducirValidator.ValidarCreacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de creación de licencia es inválida.", errors);

        var clienteExistente = await _clienteDataService.ObtenerPorIdAsync(request.CLI_id, cancellationToken);
        if (clienteExistente is null)
            throw new NotFoundException("El cliente asociado no existe.");

        var dataModel = LicenciaConducirBusinessMapper.ToDataModel(request);
        var creado = await _licenciaDataService.CrearAsync(dataModel, cancellationToken);

        return LicenciaConducirBusinessMapper.ToResponse(creado);
    }

    public async Task<LicenciaConducirResponse> ActualizarAsync(ActualizarLicenciaConducirRequest request, CancellationToken cancellationToken = default)
    {
        var errors = LicenciaConducirValidator.ValidarActualizacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de licencia es inválida.", errors);

        var existente = await _licenciaDataService.ObtenerPorIdAsync(request.LIC_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró la licencia solicitada.");

        var clienteExistente = await _clienteDataService.ObtenerPorIdAsync(request.CLI_id, cancellationToken);
        if (clienteExistente is null)
            throw new NotFoundException("El cliente asociado no existe.");

        var dataModel = LicenciaConducirBusinessMapper.ToDataModel(request);

        // Conservar campos de auditoría de creación
        dataModel.LIC_fechaCreacion = existente.LIC_fechaCreacion;
        dataModel.LIC_usuarioCreacion = existente.LIC_usuarioCreacion;

        var actualizado = await _licenciaDataService.ActualizarAsync(dataModel, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar la licencia porque no existe.");

        return LicenciaConducirBusinessMapper.ToResponse(actualizado);
    }

    public async Task<LicenciaConducirResponse> ObtenerPorIdAsync(Guid licenciaId, CancellationToken cancellationToken = default)
    {
        var licencia = await _licenciaDataService.ObtenerPorIdAsync(licenciaId, cancellationToken);

        if (licencia is null)
            throw new NotFoundException("No se encontró la licencia solicitada.");

        return LicenciaConducirBusinessMapper.ToResponse(licencia);
    }

    public async Task<IReadOnlyList<LicenciaConducirResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var licencias = await _licenciaDataService.ListarAsync(cancellationToken);

        return licencias.Select(LicenciaConducirBusinessMapper.ToResponse).ToList();
    }
}