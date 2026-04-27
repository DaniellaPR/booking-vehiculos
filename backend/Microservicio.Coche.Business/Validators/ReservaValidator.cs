
using Microservicios.Coche.Business.DTOs.Reserva;

namespace Microservicios.Coche.Business.Validators;

public static class ReservaValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearReservaRequest request)
    {
        var errors = new List<string>();

        if (request.CLI_id == Guid.Empty)
            errors.Add("El ID del cliente es obligatorio.");

        if (request.RES_sucursalRetiroId == Guid.Empty)
            errors.Add("La sucursal de retiro es obligatoria.");

        if (request.RES_sucursalEntregaId == Guid.Empty)
            errors.Add("La sucursal de entrega es obligatoria.");

        if (request.RES_fechaRetiro >= request.RES_fechaEntrega)
            errors.Add("La fecha de retiro debe ser anterior a la fecha de entrega.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarReservaRequest request)
    {
        var errors = new List<string>();

        if (request.RES_id == Guid.Empty)
            errors.Add("El ID de la reserva es inválido.");

        if (request.CLI_id == Guid.Empty)
            errors.Add("El ID del cliente es obligatorio.");

        if (request.RES_sucursalRetiroId == Guid.Empty)
            errors.Add("La sucursal de retiro es obligatoria.");

        if (request.RES_sucursalEntregaId == Guid.Empty)
            errors.Add("La sucursal de entrega es obligatoria.");

        if (request.RES_fechaRetiro >= request.RES_fechaEntrega)
            errors.Add("La fecha de retiro debe ser anterior a la fecha de entrega.");

        if (string.IsNullOrWhiteSpace(request.RES_estado))
            errors.Add("El estado de la reserva es obligatorio.");

        return errors;
    }
}