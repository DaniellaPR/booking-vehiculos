
using Microservicios.Coche.Business.DTOs.UsuarioApp;
using System.Text.RegularExpressions;

namespace Microservicios.Coche.Business.Validators;

public static class UsuarioAppValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearUsuarioAppRequest request)
    {
        var errors = new List<string>();

        if (request.ROL_id == Guid.Empty)
            errors.Add("El ID del rol es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.USU_email))
            errors.Add("El correo electrónico es obligatorio.");
        else if (!Regex.IsMatch(request.USU_email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add("El formato del correo electrónico es inválido.");

        if (string.IsNullOrWhiteSpace(request.USU_passwordHash))
            errors.Add("La contraseña (hash) es obligatoria.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarUsuarioAppRequest request)
    {
        var errors = new List<string>();

        if (request.USU_id == Guid.Empty)
            errors.Add("El ID del usuario es inválido.");

        if (request.ROL_id == Guid.Empty)
            errors.Add("El ID del rol es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.USU_email))
            errors.Add("El correo electrónico es obligatorio.");
        else if (!Regex.IsMatch(request.USU_email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            errors.Add("El formato del correo electrónico es inválido.");

        if (string.IsNullOrWhiteSpace(request.USU_passwordHash))
            errors.Add("La contraseña (hash) es obligatoria.");

        return errors;
    }
}