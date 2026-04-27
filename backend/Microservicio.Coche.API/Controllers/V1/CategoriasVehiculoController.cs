using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.CategoriaVehiculo;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/categorias-vehiculo")]
[Authorize(Roles = "ADMIN,VENDEDOR")]  // Default: solo admin/vendedor
public class CategoriasVehiculoController : ControllerBase
{
    private readonly ICategoriaVehiculoService _categoriaService;

    public CategoriasVehiculoController(ICategoriaVehiculoService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    // ── PÚBLICO — el marketplace necesita ver categorías sin token ──
    [HttpGet]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<CategoriaVehiculoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _categoriaService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<CategoriaVehiculoResponse>>.Ok(result, "Consulta exitosa."));
    }

    // ── PÚBLICO — detalle de categoría también público ──
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoriaService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Consulta exitosa."));
    }

    // ── PROTEGIDOS — solo admin/vendedor pueden modificar ──
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Crear([FromBody] CrearCategoriaVehiculoRequest request, CancellationToken cancellationToken)
    {
        request.CAT_usuarioCreacion = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";
        request.CAT_creadoDesdeIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.CAT_creadoDesdeServicio = "Microservicio.Coche.Api";
        request.CAT_creadoDesdeEquipo = Environment.MachineName;

        var result = await _categoriaService.CrearAsync(request, cancellationToken);
        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Categoría creada exitosamente."));
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoriaVehiculoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarCategoriaVehiculoRequest request, CancellationToken cancellationToken)
    {
        request.CAT_id = id;
        request.CAT_usuarioModificacion = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";
        request.CAT_modificadoDesdeIp = HttpContext.Connection.RemoteIpAddress?.ToString();
        request.CAT_modificadoDesdeServicio = "Microservicio.Coche.Api";
        request.CAT_modificadoDesdeEquipo = Environment.MachineName;

        var result = await _categoriaService.ActualizarAsync(request, cancellationToken);
        return Ok(ApiResponse<CategoriaVehiculoResponse>.Ok(result, "Categoría actualizada exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    public async Task<IActionResult> EliminarLogico(Guid id, CancellationToken cancellationToken)
    {
        var usuario = User.Identity?.Name ?? User.FindFirst("unique_name")?.Value ?? "api_user";
        await _categoriaService.EliminarLogicoAsync(id, usuario, cancellationToken);
        return Ok(ApiResponse<string>.Ok("OK", "Categoría eliminada lógicamente."));
    }
}