using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Tarifa;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/tarifas")]
[Authorize]
public class TarifasController : ControllerBase
{
    private readonly ITarifaService _tarifaService;
    public TarifasController(ITarifaService tarifaService) => _tarifaService = tarifaService;

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
        => Ok(ApiResponse<IReadOnlyList<TarifaResponse>>.Ok(await _tarifaService.ListarAsync(ct), "Consulta exitosa."));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken ct)
        => Ok(ApiResponse<TarifaResponse>.Ok(await _tarifaService.ObtenerPorIdAsync(id, ct), "Consulta exitosa."));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearTarifaRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        request.TAR_usuarioCreacion = User.FindFirst("unique_name")?.Value ?? "api_user";
        return Ok(ApiResponse<TarifaResponse>.Ok(await _tarifaService.CrearAsync(request, ct), "Tarifa creada exitosamente."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarTarifaRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        request.TAR_id = id;
        return Ok(ApiResponse<TarifaResponse>.Ok(await _tarifaService.ActualizarAsync(request, ct), "Tarifa actualizada exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(Guid id, CancellationToken ct)
    {
        await _tarifaService.EliminarAsync(id, ct);
        return NoContent();
    }
}