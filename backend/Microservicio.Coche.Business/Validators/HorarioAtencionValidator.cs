using Microservicios.Coche.Business.DTOs.HorarioAtencion;

namespace Microservicios.Coche.Business.Validators;

public static class HorarioAtencionValidator
{
    public static IReadOnlyCollection<string> ValidarCreacion(CrearHorarioAtencionRequest request)
    {
        var errors = new List<string>();

        if (request.SUC_id == Guid.Empty)
            errors.Add("El ID de la sucursal es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.HOR_diaSemana))
            errors.Add("El día de la semana es obligatorio.");

        if (request.HOR_apertura >= request.HOR_cierre)
            errors.Add("La hora de apertura debe ser menor a la hora de cierre.");

        return errors;
    }

    public static IReadOnlyCollection<string> ValidarActualizacion(ActualizarHorarioAtencionRequest request)
    {
        var errors = new List<string>();

        if (request.HOR_id == Guid.Empty)
            errors.Add("El ID del horario es inválido.");

        if (request.SUC_id == Guid.Empty)
            errors.Add("El ID de la sucursal es obligatorio.");

        if (string.IsNullOrWhiteSpace(request.HOR_diaSemana))
            errors.Add("El día de la semana es obligatorio.");

        if (request.HOR_apertura >= request.HOR_cierre)
            errors.Add("La hora de apertura debe ser menor a la hora de cierre.");

        return errors;
    }
}