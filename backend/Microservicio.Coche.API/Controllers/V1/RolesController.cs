using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microservicios.Coche.Api.Models.Common;
using Microservicios.Coche.Business.DTOs.Rol;
using Microservicios.Coche.Business.Interfaces;

namespace Microservicios.Coche.Api.Controllers.V1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/roles")]
[AllowAnonymous]
public class RolesController : ControllerBase
{
    private readonly IRolService _rolService;

    public RolesController(IRolService rolService)
    {
        _rolService = rolService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IReadOnlyList<RolResponse>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _rolService.ListarAsync(cancellationToken);
        return Ok(ApiResponse<IReadOnlyList<RolResponse>>.Ok(result, "Roles consultados exitosamente."));
    }
}

