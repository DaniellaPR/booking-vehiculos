using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Reserva; // DTOs antiguos y nuevos
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reservas")]
[AllowAnonymous] // Lo mantenemos como lo tenías por ahora
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    // ====================================================================
    // ENDPOINTS PARA TU DASHBOARD ANGULAR (Mantienen tu lógica original)
    // ====================================================================

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _reservaService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ReservaResponse>>.Ok(result, "Consulta de reservas exitosa."));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _reservaService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva encontrada."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarReservaRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errores));
        }
        request.RES_id = id;
        var result = await _reservaService.ActualizarAsync(request, cancellationToken);
        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva actualizada exitosamente."));
    }

    // ====================================================================
    // ENDPOINTS EXIGIDOS POR EL CONTRATO (YAML) DEL BOOKING
    // ====================================================================

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] ReservaRequestDto request, CancellationToken cancellationToken)
    {
        // Usamos el DTO del contrato (ReservaRequestDto) en lugar de CrearReservaRequest
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errores));
        }

        // Llamada al servicio que implementará la lógica matemática de solapamiento
        var result = await _reservaService.CrearReservaAsync(request, cancellationToken);
        return Ok(ApiResponse<ReservaResponseDto>.Ok(result, "Reserva creada exitosamente."));
    }

    [HttpPost("{id:guid}/alquilar")]
    public async Task<IActionResult> IniciarAlquiler(Guid id, [FromBody] AlquilerRequestDto request, CancellationToken cancellationToken)
    {
        // En tu capa Business debes crear este método en tu IReservaService
        await _reservaService.IniciarAlquilerAsync(id, request.KmSalida, cancellationToken);
        return Ok(ApiResponse<string>.Ok("OK", "Alquiler iniciado."));
    }

    [HttpPost("{id:guid}/devolver")]
    public async Task<IActionResult> RegistrarDevolucion(Guid id, [FromBody] DevolucionRequestDto request, CancellationToken cancellationToken)
    {
        // En tu capa Business debes crear este método en tu IReservaService
        await _reservaService.RegistrarDevolucionAsync(id, request.KmEntrada, request.CargoExtra, cancellationToken);
        return Ok(ApiResponse<string>.Ok("OK", "Vehículo devuelto exitosamente."));
    }
}