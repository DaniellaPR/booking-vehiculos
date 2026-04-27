using System.Text.Json.Serialization;
using Microservicios.Coche.Business.DTOs.ExtraAdicional;

namespace Microservicios.Coche.Business.Validators;

public static class ExtraAdicionalValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearExtraAdicionalRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.EXT_nombre))
            errors.Add("El nombre del extra adicional es obligatorio.");

        if (request.EXT_costo < 0)
            errors.Add("El costo del extra adicional no puede ser negativo.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarExtraAdicionalRequest request)
    {
        var errors = new List<string>();

        if (request.EXT_id == Guid.Empty)
            errors.Add("El ID del extra adicional es inválido.");

        if (string.IsNullOrWhiteSpace(request.EXT_nombre))
            errors.Add("El nombre del extra adicional es obligatorio.");

        if (request.EXT_costo < 0)
            errors.Add("El costo del extra adicional no puede ser negativo.");

        return errors;
    }
}