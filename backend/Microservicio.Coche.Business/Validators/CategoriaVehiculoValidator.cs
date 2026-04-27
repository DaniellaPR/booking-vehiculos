
using Microservicios.Coche.Business.DTOs.CategoriaVehiculo;

namespace Microservicios.Coche.Business.Validators;

public static class CategoriaVehiculoValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearCategoriaVehiculoRequest request)
    {
        var errors = new List<string>();

        if (string.IsNullOrWhiteSpace(request.CAT_nombre))
            errors.Add("El nombre de la categoría es obligatorio.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarCategoriaVehiculoRequest request)
    {
        var errors = new List<string>();

        if (request.CAT_id == Guid.Empty)
            errors.Add("El ID de la categoría es inválido.");

        if (string.IsNullOrWhiteSpace(request.CAT_nombre))
            errors.Add("El nombre de la categoría es obligatorio.");

        return errors;
    }
}