using Microservicios.Coche.Business.DTOs.Rol;

namespace Microservicios.Coche.Business.Validators;

public static class RolValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearRolRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.ROL_nombre))
            errors.Add("El nombre del rol es obligatorio.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarRolRequest request)
    {
        var errors = new List<string>();

        if (request.ROL_id == Guid.Empty)
            errors.Add("El ID del rol es inválido.");

        if (string.IsNullOrWhiteSpace(request.ROL_nombre))
            errors.Add("El nombre del rol es obligatorio.");

        return errors;
    }
}