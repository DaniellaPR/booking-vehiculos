using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.UsuarioApp;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/usuarios-app")]
[Authorize]
public class UsuariosAppController : ControllerBase
{
    private readonly IUsuarioAppService _usuarioAppService;
    public UsuariosAppController(IUsuarioAppService usuarioAppService) => _usuarioAppService = usuarioAppService;

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
        => Ok(ApiResponse<IReadOnlyList<UsuarioAppResponse>>.Ok(await _usuarioAppService.ListarAsync(ct), "Consulta exitosa."));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken ct)
        => Ok(ApiResponse<UsuarioAppResponse>.Ok(await _usuarioAppService.ObtenerPorIdAsync(id, ct), "Consulta exitosa."));

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearUsuarioAppRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        return Ok(ApiResponse<UsuarioAppResponse>.Ok(await _usuarioAppService.CrearAsync(request, ct), "Usuario creado exitosamente."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarUsuarioAppRequest request, CancellationToken ct)
    {
        if (!ModelState.IsValid)
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList()));
        request.USU_id = id;
        return Ok(ApiResponse<UsuarioAppResponse>.Ok(await _usuarioAppService.ActualizarAsync(request, ct), "Usuario actualizado exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(Guid id, CancellationToken ct)
    {
        await _usuarioAppService.EliminarAsync(id, ct);
        return NoContent();
    }
}