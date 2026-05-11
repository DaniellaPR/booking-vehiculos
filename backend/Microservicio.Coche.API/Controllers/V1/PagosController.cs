using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Pago;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.DataManagement.Common;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/pagos")]
[AllowAnonymous] // Cambiar a [Authorize] según tus reglas de seguridad luego
public class PagosController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagosController(IPagoService pagoService)
    {
        _pagoService = pagoService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<PagoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _pagoService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<PagoResponse>>.Ok(result, "Consulta de pagos exitosa."));
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<PagoResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> ObtenerPorId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _pagoService.ObtenerPorIdAsync(id, cancellationToken);
        return Ok(ApiResponse<PagoResponse>.Ok(result, "Pago encontrado."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<PagoResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Crear([FromBody] CrearPagoRequest request, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            return BadRequest(ApiResponse<object>.Fail("Datos inválidos.", errores));
        }

        var result = await _pagoService.CrearAsync(request, cancellationToken);
        return Ok(ApiResponse<PagoResponse>.Ok(result, "Pago registrado exitosamente."));
    }

    [HttpPost("buscar")]
    [ProducesResponseType(typeof(ApiResponse<DataPagedResult<PagoResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Buscar([FromBody] PagoFiltroRequest request, CancellationToken cancellationToken)
    {
        var result = await _pagoService.BuscarAsync(request, cancellationToken);
        return Ok(ApiResponse<DataPagedResult<PagoResponse>>.Ok(result, "Consulta paginada exitosa."));
    }
}