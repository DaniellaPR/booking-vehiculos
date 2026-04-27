
using Microservicios.Coche.Business.DTOs.ReservaDetalle;

namespace Microservicios.Coche.Business.Validators;

public static class ReservaDetalleValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearReservaDetalleRequest request)
    {
        var errors = new List<string>();

        if (request.RES_id == Guid.Empty)
            errors.Add("El ID de la reserva es obligatorio.");

        // Validamos la regla CHECK de la base de datos: Debe tener al menos Seguro O Extra
        if (!request.SEG_id.HasValue && !request.EXT_id.HasValue)
            errors.Add("El detalle debe contener al menos un Seguro o un Extra Adicional.");

        if (request.REX_cantidad <= 0)
            errors.Add("La cantidad debe ser mayor a cero.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarReservaDetalleRequest request)
    {
        var errors = new List<string>();

        if (request.REX_id == Guid.Empty)
            errors.Add("El ID del detalle es inválido.");

        if (request.RES_id == Guid.Empty)
            errors.Add("El ID de la reserva es obligatorio.");

        if (!request.SEG_id.HasValue && !request.EXT_id.HasValue)
            errors.Add("El detalle debe contener al menos un Seguro o un Extra Adicional.");

        if (request.REX_cantidad <= 0)
            errors.Add("La cantidad debe ser mayor a cero.");

        return errors;
    }
}