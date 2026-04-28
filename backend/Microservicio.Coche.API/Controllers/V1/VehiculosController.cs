using Asp.Versioning;
using Microservicios.coche.Business.Interfaces;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Vehiculo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/vehiculos")]
[AllowAnonymous]
public class VehiculosController : ControllerBase
{
    private readonly IVehiculoService _vehiculoService;

    public VehiculosController(IVehiculoService vehiculoService)
    {
        _vehiculoService = vehiculoService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<VehiculoResponse>>.Ok(result, "Consulta de vehículos exitosa."));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _vehiculoService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Vehículo encontrado."));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearVehiculoRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errores));
        }
        request.VEH_usuarioCreacion ??= "api_user";
        var result = await _vehiculoService.CrearAsync(request, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Vehículo incorporado a la flota."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarVehiculoRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errores));
        }
        request.VEH_id = id;
        var result = await _vehiculoService.ActualizarAsync(request, cancellationToken);
        return Ok(ApiResponse<VehiculoResponse>.Ok(result, "Vehículo actualizado exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Eliminar(Guid id, CancellationToken cancellationToken)
    {
        await _vehiculoService.EliminarAsync(id, cancellationToken);
        return Ok(ApiResponse<string>.Ok("OK", "Vehículo eliminado físicamente con éxito."));
    }
}