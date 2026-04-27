
using Microservicios.Coche.Business.DTOs.Tarifa;

namespace Microservicios.Coche.Business.Validators;

public static class TarifaValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearTarifaRequest request)
    {
        var errors = new List<string>();

        if (request.CAT_id == Guid.Empty)
            errors.Add("El ID de la categoría es obligatorio.");

        if (request.TAR_precioDiario <= 0)
            errors.Add("El precio diario de la tarifa debe ser mayor a cero.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarTarifaRequest request)
    {
        var errors = new List<string>();

        if (request.TAR_id == Guid.Empty)
            errors.Add("El ID de la tarifa es inválido.");

        if (request.CAT_id == Guid.Empty)
            errors.Add("El ID de la categoría es obligatorio.");

        if (request.TAR_precioDiario <= 0)
            errors.Add("El precio diario de la tarifa debe ser mayor a cero.");

        return errors;
    }
}