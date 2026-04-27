using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Seguro;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/seguros")]
[Authorize(Roles = "ADMIN,VENDEDOR")]
public class SegurosController : ControllerBase
{
    private readonly ISeguroService _seguroService;

    public SegurosController(ISeguroService seguroService)
    {
        _seguroService = seguroService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SeguroResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _seguroService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<SeguroResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<SeguroResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _seguroService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<SeguroResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<SeguroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearSeguroRequest request, CancellationToken cancellationToken)
    {
        // 🚨 AQUÍ ESTABA EL ERROR: Solo asignamos el usuario, borramos lo de la IP y Equipo
        request.SEG_usuarioCreacion = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        var result = await _seguroService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<SeguroResponse>.Ok(result, "Seguro creado exitosamente."));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<SeguroResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarSeguroRequest request, CancellationToken cancellationToken)
    {
        request.SEG_id = id;

        // 🚨 Igual aquí: Solo asignamos el usuario que modifica
        request.SEG_usuarioModificacion = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        var result = await _seguroService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<SeguroResponse>.Ok(result, "Seguro actualizado exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> EliminarLogico(Guid id, CancellationToken cancellationToken)
    {
        var usuario = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        await _seguroService.EliminarLogicoAsync(id, usuario, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Seguro eliminado lógicamente."));
    }
}

