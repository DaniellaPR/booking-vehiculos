using Microservicios.Coche.Business.DTOs.Auditoria;

namespace Microservicios.Coche.Business.Validators;

public static class AuditoriaValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearAuditoriaRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.AUD_nombreTabla))
            errors.Add("El nombre de la tabla es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.AUD_operacion))
            errors.Add("La operación es obligatoria.");

        return errors;
    }
}