using Microservicios.Coche.Business.DTOs.CategoriaVehiculo;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class CategoriaVehiculoService : ICategoriaVehiculoService
{
    private readonly ICategoriaVehiculoDataService _dataService;

    public CategoriaVehiculoService(ICategoriaVehiculoDataService dataService)
    {
        _dataService = dataService;
    }

    // ✅ FIX: Antes devolvía new List<>() vacía hardcodeada
    public async Task<IReadOnlyList<CategoriaVehiculoResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var categorias = await _dataService.ListarAsync(cancellationToken);
        return categorias.Select(c => c.ToDto()).ToList();
    }

    // ✅ FIX: Antes devolvía new CategoriaVehiculoResponse() vacío hardcodeado
    public async Task<CategoriaVehiculoResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var categoria = await _dataService.ObtenerPorIdAsync(id, cancellationToken);

        if (categoria is null)
            throw new NotFoundException("No se encontró la categoría de vehículo solicitada.");

        return categoria.ToDto();
    }

    // ✅ FIX: Antes devolvía new CategoriaVehiculoResponse() vacío sin guardar nada
    public async Task<CategoriaVehiculoResponse> CrearAsync(CrearCategoriaVehiculoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = CategoriaVehiculoValidator.ValidarCreacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de creación de categoría es inválida.", errors);

        var dataModel = request.ToDataModel();
        var creado = await _dataService.CrearAsync(dataModel, cancellationToken);

        return creado.ToDto();
    }

    // ✅ FIX: Antes devolvía new CategoriaVehiculoResponse() vacío sin guardar nada
    public async Task<CategoriaVehiculoResponse> ActualizarAsync(ActualizarCategoriaVehiculoRequest request, CancellationToken cancellationToken = default)
    {
        var errors = CategoriaVehiculoValidator.ValidarActualizacion(request);

        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de categoría es inválida.", errors);

        var existente = await _dataService.ObtenerPorIdAsync(request.CAT_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró la categoría de vehículo a actualizar.");

        var dataModel = request.ToDataModel(existente);
        var actualizado = await _dataService.ActualizarAsync(dataModel, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar la categoría porque no existe.");

        return actualizado.ToDto();
    }

    public Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
        => Task.CompletedTask;
}