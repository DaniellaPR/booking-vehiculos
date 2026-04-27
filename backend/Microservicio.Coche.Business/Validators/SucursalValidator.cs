using Microservicios.Coche.Business.DTOs.Sucursal;

namespace Microservicios.Coche.Business.Validators;

public static class SucursalValidator
{
    public static List<string> ValidarCreacion(CrearSucursalRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.SUC_nombre))
            errors.Add("El nombre de la sucursal es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.SUC_ciudad))
            errors.Add("La ciudad de la sucursal es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.SUC_direccion))
            errors.Add("La dirección de la sucursal es obligatoria.");

        return errors;
    }

    public static List<string> ValidarActualizacion(ActualizarSucursalRequest request)
    {
        var errors = new List<string>();

        // En actualización SÍ se valida el ID
        if (request.SucursalId == Guid.Empty)
            errors.Add("El ID de la sucursal es inválido o no fue proporcionado.");

        if (string.IsNullOrWhiteSpace(request.SUC_nombre))
            errors.Add("El nombre de la sucursal es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.SUC_ciudad))
            errors.Add("La ciudad de la sucursal es obligatoria.");

        if (string.IsNullOrWhiteSpace(request.SUC_direccion))
            errors.Add("La dirección de la sucursal es obligatoria.");

        return errors;
    }
}