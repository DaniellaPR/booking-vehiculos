
using Microservicios.Coche.Business.DTOs.Seguro;

namespace Microservicios.Coche.Business.Validators;

public static class SeguroValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearSeguroRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.SEG_nombre))
            errors.Add("El nombre del seguro es obligatorio.");

        if (request.SEG_costoDiario < 0)
            errors.Add("El costo diario del seguro no puede ser negativo.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarSeguroRequest request)
    {
        var errors = new List<string>();

        if (request.SEG_id == Guid.Empty)
            errors.Add("El ID del seguro es inválido.");

        if (string.IsNullOrWhiteSpace(request.SEG_nombre))
            errors.Add("El nombre del seguro es obligatorio.");

        if (request.SEG_costoDiario < 0)
            errors.Add("El costo diario del seguro no puede ser negativo.");

        return errors;
    }
}