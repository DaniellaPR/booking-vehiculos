using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Sucursal;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sucursales")]
[AllowAnonymous]
public class SucursalesController : ControllerBase
{
    private readonly ISucursalService _sucursalService;

    public SucursalesController(ISucursalService sucursalService)
    {
        _sucursalService = sucursalService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<SucursalResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _sucursalService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<SucursalResponse>>.Ok(result, "Consulta exitosa."));
    }

    // 🚨 Corregido de {id:long} a {id:guid}
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<SucursalResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _sucursalService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<SucursalResponse>.Ok(result, "Consulta exitosa."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<SucursalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearSucursalRequest request, CancellationToken cancellationToken)
    {
        // Asignamos solo el campo de auditoría que sí existe en nuestro DTO
        request.CreadoPorUsuario = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        var result = await _sucursalService.CrearAsync(request, cancellationToken);

        return Ok(ApiResponse<SucursalResponse>.Ok(result, "Sucursal creada exitosamente."));
    }

    // 🚨 Corregido de {id:long} a {id:guid}
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<SucursalResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarSucursalRequest request, CancellationToken cancellationToken)
    {
        request.SucursalId = id; // Enlazamos el GUID de la URL al DTO
        request.ModificadoPorUsuario = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        var result = await _sucursalService.ActualizarAsync(request, cancellationToken);

        return Ok(ApiResponse<SucursalResponse>.Ok(result, "Sucursal actualizada exitosamente."));
    }

    // 🚨 Corregido de {id:long} a {id:guid}
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> EliminarLogico(Guid id, CancellationToken cancellationToken)
    {
        var usuario = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";

        await _sucursalService.EliminarLogicoAsync(id, usuario, cancellationToken);

        return Ok(ApiResponse<string>.Ok("OK", "Sucursal eliminada lógicamente."));
    }
}

