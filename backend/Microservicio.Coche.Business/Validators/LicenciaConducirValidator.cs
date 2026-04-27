using Microservicios.Coche.Business.DTOs.LicenciaConducir;

namespace Microservicios.Coche.Business.Validators;

public static class LicenciaConducirValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearLicenciaConducirRequest request)
    {
        var errors = new List<string>();

        if (request.CLI_id == Guid.Empty)
            errors.Add("El ID del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.LIC_numero))
            errors.Add("El número de licencia es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.LIC_categoria))
            errors.Add("La categoría de la licencia es obligatoria.");

        if (request.LIC_vigencia == default)
            errors.Add("La fecha de vigencia es inválida.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarLicenciaConducirRequest request)
    {
        var errors = new List<string>();

        if (request.LIC_id == Guid.Empty)
            errors.Add("El ID de la licencia es inválido.");

        if (request.CLI_id == Guid.Empty)
            errors.Add("El ID del cliente es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.LIC_numero))
            errors.Add("El número de licencia es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.LIC_categoria))
            errors.Add("La categoría de la licencia es obligatoria.");

        return errors;
    }
}