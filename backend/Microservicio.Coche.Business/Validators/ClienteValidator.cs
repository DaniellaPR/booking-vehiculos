using Microservicios.Coche.Business.DTOs.Cliente;

namespace Microservicios.Coche.Business.Validators;

public static class ClienteValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearClienteRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.CLI_nombres))
            errors.Add("Los nombres del cliente son obligatorios.");

        if (string.IsNullOrWhiteSpace(request.CLI_apellidos))
            errors.Add("Los apellidos del cliente son obligatorios.");

        if (string.IsNullOrWhiteSpace(request.CLI_cedula))
            errors.Add("La cédula es obligatoria.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarClienteRequest request)
    {
        var errors = new List<string>();

        if (request.CLI_id == Guid.Empty)
            errors.Add("El ID del cliente es inválido.");

        if (string.IsNullOrWhiteSpace(request.CLI_nombres))
            errors.Add("Los nombres del cliente son obligatorios.");

        if (string.IsNullOrWhiteSpace(request.CLI_apellidos))
            errors.Add("Los apellidos del cliente son obligatorios.");

        if (string.IsNullOrWhiteSpace(request.CLI_cedula))
            errors.Add("La cédula es obligatoria.");

        return errors;
    }
}