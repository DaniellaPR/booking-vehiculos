
using Microservicios.Coche.Business.DTOs.Vehiculo;

namespace Microservicios.Coche.Business.Validators;

public static class VehiculoValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearVehiculoRequest request)
    {
        var errors = new List<string>();

        if (request.CAT_id == Guid.Empty)
            errors.Add("El ID de la categoría es obligatorio.");

        if (request.SUC_id == Guid.Empty)
            errors.Add("El ID de la sucursal es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.VEH_placa))
            errors.Add("La placa del vehículo es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.VEH_modelo))
            errors.Add("El modelo del vehículo es obligatorio.");

        if (request.VEH_anio < 1900 || request.VEH_anio > DateTime.UtcNow.Year + 1)
            errors.Add("El año del vehículo no es válido.");

        if (request.VEH_kilometraje < 0)
            errors.Add("El kilometraje no puede ser negativo.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarVehiculoRequest request)
    {
        var errors = new List<string>();

        if (request.VEH_id == Guid.Empty)
            errors.Add("El ID del vehículo es inválido.");

        if (request.CAT_id == Guid.Empty)
            errors.Add("El ID de la categoría es obligatorio.");

        if (request.SUC_id == Guid.Empty)
            errors.Add("El ID de la sucursal es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.VEH_placa))
            errors.Add("La placa del vehículo es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.VEH_modelo))
            errors.Add("El modelo del vehículo es obligatorio.");

        if (request.VEH_anio < 1900 || request.VEH_anio > DateTime.UtcNow.Year + 1)
            errors.Add("El año del vehículo no es válido.");

        if (request.VEH_kilometraje < 0)
            errors.Add("El kilometraje no puede ser negativo.");

        return errors;
    }
}