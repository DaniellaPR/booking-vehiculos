using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.Business.DTOs.Seguro;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Mappers;

namespace Microservicios.Coche.Business.Services;

public class SeguroService : ISeguroService
{
    private readonly ISeguroDataService _seguroDataService;

    public SeguroService(ISeguroDataService seguroDataService)
    {
        _seguroDataService = seguroDataService;
    }

    public async Task<SeguroResponse> CrearAsync(CrearSeguroRequest request, CancellationToken cancellationToken = default)
    {
        // NOTA: Si creas un SeguroValidator, iría aquí:
        // var errors = SeguroValidator.ValidarCreacion(request);
        // if (errors.Any()) throw new ValidationException("Solicitud inválida.", errors);

        var dataModel = request.ToDataModel();
        var creado = await _seguroDataService.CrearAsync(dataModel, cancellationToken);

        return creado.ToResponse();
    }

    public async Task<SeguroResponse> ActualizarAsync(ActualizarSeguroRequest request, CancellationToken cancellationToken = default)
    {
        var existente = await _seguroDataService.ObtenerPorIdAsync(request.SEG_id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró el seguro solicitado.");

        // Ojo aquí: asumiendo que tu mapper tiene el método para no pisar la auditoría
        // O lo hacemos manualmente como en tu ClienteService:
        existente.SEG_nombre = request.SEG_nombre;
        existente.SEG_costoDiario = request.SEG_costoDiario;
        existente.SEG_usuarioModificacion = request.SEG_usuarioModificacion;

        var actualizado = await _seguroDataService.ActualizarAsync(existente, cancellationToken);

        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el seguro porque no existe.");

        return actualizado.ToResponse();
    }

    public async Task<SeguroResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var seguro = await _seguroDataService.ObtenerPorIdAsync(id, cancellationToken);

        if (seguro is null)
            throw new NotFoundException("No se encontró el seguro solicitado.");

        return seguro.ToResponse();
    }

    public async Task<IReadOnlyList<SeguroResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var seguros = await _seguroDataService.ListarAsync(cancellationToken);

        return seguros.Select(s => s.ToResponse()).ToList();
    }

    public async Task EliminarLogicoAsync(Guid id, string usuario, CancellationToken cancellationToken = default)
    {
        var existente = await _seguroDataService.ObtenerPorIdAsync(id, cancellationToken);

        if (existente is null)
            throw new NotFoundException("No se encontró el seguro a eliminar.");

        await _seguroDataService.EliminarLogicoAsync(id, usuario, cancellationToken);
    }
}