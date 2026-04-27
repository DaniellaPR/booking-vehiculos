using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Reserva;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reservas")]
[AllowAnonymous]
public class ReservasController : ControllerBase
{
    private readonly IReservaService _reservaService;

    public ReservasController(IReservaService reservaService)
    {
        _reservaService = reservaService;
    }

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

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearReservaRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errores));
        }
        request.RES_usuarioCreacion ??= "api_user";
        var result = await _reservaService.CrearAsync(request, cancellationToken);
        return Ok(ApiResponse<ReservaResponse>.Ok(result, "Reserva creada exitosamente."));
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
}