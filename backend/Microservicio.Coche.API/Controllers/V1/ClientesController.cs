using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Cliente;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/clientes")]
public class ClientesController : ControllerBase
{
    private readonly IClienteService _clienteService;

    public ClientesController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _clienteService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<ClienteResponse>>.Ok(result));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _clienteService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<ClienteResponse>.Ok(result));
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearClienteRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos de entrada inválidos.", errores));
        }

        var result = await _clienteService.CrearAsync(request, cancellationToken);
        return CreatedAtAction(nameof(ObtenerPorId), new { id = result.CLI_id },
            ApiResponse<ClienteResponse>.Ok(result, "Cliente creado exitosamente."));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Actualizar(Guid id, [FromBody] ActualizarClienteRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos de entrada inválidos.", errores));
        }

        request.CLI_id = id;
        var result = await _clienteService.ActualizarAsync(request, cancellationToken);
        return Ok(ApiResponse<ClienteResponse>.Ok(result, "Cliente actualizado exitosamente."));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Eliminar(Guid id, CancellationToken cancellationToken)
    {
        await _clienteService.EliminarAsync(id, cancellationToken);
        return NoContent();
    }
}