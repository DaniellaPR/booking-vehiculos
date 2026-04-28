using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.ExtraAdicional;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
// 🚨 ESTA ES LA LÍNEA CLAVE: Definimos la ruta explícitamente con guión
[Route("api/v{version:apiVersion}/extras-adicionales")]
[AllowAnonymous]
public class ExtraAdicionalesController : ControllerBase
{
    private readonly IExtraAdicionalService _extraAdicionalService;

    public ExtraAdicionalesController(IExtraAdicionalService extraAdicionalService)
    {
        _extraAdicionalService = extraAdicionalService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<ExtraAdicionalResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _extraAdicionalService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ExtraAdicionalResponse>>.Ok(result, "Consulta exitosa."));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ExtraAdicionalResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _extraAdicionalService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<ExtraAdicionalResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ExtraAdicionalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearExtraAdicionalRequest request, CancellationToken cancellationToken)
    {
        // Solo el campo de auditoría que definimos en el DTO
        request.EXT_usuarioCreacion = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        var result = await _extraAdicionalService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<ExtraAdicionalResponse>.Ok(result, "Extra adicional creado exitosamente."));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ExtraAdicionalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarExtraAdicionalRequest request, CancellationToken cancellationToken)
    {
        request.EXT_id = id;
        request.EXT_usuarioModificacion = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        var result = await _extraAdicionalService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<ExtraAdicionalResponse>.Ok(result, "Extra adicional actualizado exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> EliminarLogico(Guid id, CancellationToken cancellationToken)
    {
        var usuario = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        await _extraAdicionalService.EliminarLogicoAsync(id, usuario, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Extra adicional eliminado lógicamente."));
    }
}

