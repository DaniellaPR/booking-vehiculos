
using Microservicios.Coche.Business.DTOs.Mantenimiento;

namespace Microservicios.Coche.Business.Validators;

public static class MantenimientoValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearMantenimientoRequest request)
    {
        var errors = new List<string>();

        if (request.VEH_id == Guid.Empty)
            errors.Add("El ID del vehículo es obligatorio.");

        if (request.MAN_fecha == default)
            errors.Add("La fecha de mantenimiento es inválida.");

        if (request.MAN_costo.HasValue && request.MAN_costo.Value < 0)
            errors.Add("El costo del mantenimiento no puede ser negativo.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarMantenimientoRequest request)
    {
        var errors = new List<string>();

        if (request.MAN_id == Guid.Empty)
            errors.Add("El ID de mantenimiento es inválido.");

        if (request.VEH_id == Guid.Empty)
            errors.Add("El ID del vehículo es obligatorio.");

        if (request.MAN_fecha == default)
            errors.Add("La fecha de mantenimiento es inválida.");

        if (request.MAN_costo.HasValue && request.MAN_costo.Value < 0)
            errors.Add("El costo del mantenimiento no puede ser negativo.");

        return errors;
    }
}