
using Microservicios.Coche.Business.DTOs.Tarifa;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class TarifaService : ITarifaService
{
    private readonly ITarifaDataService _tarifaDataService;
    private readonly ICategoriaVehiculoDataService _categoriaDataService; // Inyección para validar FK

    public TarifaService(
        ITarifaDataService tarifaDataService,
        ICategoriaVehiculoDataService categoriaDataService)
    {
        _tarifaDataService = tarifaDataService;
        _categoriaDataService = categoriaDataService;
    }

    public async Task<TarifaResponse> CrearAsync(CrearTarifaRequest request, CancellationToken cancellationToken = default)
    {
        var errors = TarifaValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de tarifa es inválida.", errors);

        // Validar regla de negocio: La categoría debe existir
        var categoriaExistente = await _categoriaDataService.ObtenerPorIdAsync(request.CAT_id, cancellationToken);
        if (categoriaExistente is null)
            throw new NotFoundException("La categoría asociada no existe.");

        var dataModel = TarifaBusinessMapper.ToDataModel(request);
        var creado = await _tarifaDataService.CrearAsync(dataModel, cancellationToken);

        return TarifaBusinessMapper.ToResponse(creado);
    }

    public async Task<TarifaResponse> ActualizarAsync(ActualizarTarifaRequest request, CancellationToken cancellationToken = default)
    {
        var errors = TarifaValidator.ValidarActualizacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de tarifa es inválida.", errors);

        var existente = await _tarifaDataService.ObtenerPorIdAsync(request.TAR_id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró la tarifa solicitada.");

        // Validar si la categoría cambió y si la nueva existe
        if (existente.CAT_id != request.CAT_id)
        {
            var categoriaExistente = await _categoriaDataService.ObtenerPorIdAsync(request.CAT_id, cancellationToken);
            if (categoriaExistente is null)
                throw new NotFoundException("La nueva categoría asociada no existe.");
        }

        var dataModel = TarifaBusinessMapper.ToDataModel(request);

        dataModel.TAR_fechaCreacion = existente.TAR_fechaCreacion;
        dataModel.TAR_usuarioCreacion = existente.TAR_usuarioCreacion;

        var actualizado = await _tarifaDataService.ActualizarAsync(dataModel, cancellationToken);
        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar la tarifa.");

        return TarifaBusinessMapper.ToResponse(actualizado);
    }

    public async Task<TarifaResponse> ObtenerPorIdAsync(Guid tarifaId, CancellationToken cancellationToken = default)
    {
        var tarifa = await _tarifaDataService.ObtenerPorIdAsync(tarifaId, cancellationToken);
        if (tarifa is null)
            throw new NotFoundException("No se encontró la tarifa solicitada.");

        return TarifaBusinessMapper.ToResponse(tarifa);
    }

    public async Task<IReadOnlyList<TarifaResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var tarifas = await _tarifaDataService.ListarAsync(cancellationToken);
        return tarifas.Select(TarifaBusinessMapper.ToResponse).ToList();
    }
    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existente = await _tarifaDataService.ObtenerPorIdAsync(id, cancellationToken);
        if (existente is null)
            throw new NotFoundException($"No se encontró la tarifa con ID {id}.");
        return await _tarifaDataService.EliminarAsync(id, cancellationToken);
    }
}