using Microservicios.Coche.Business.DTOs.Pago;

namespace Microservicios.Coche.Business.Validators;

public static class PagoValidator
{
    public static List<string> ValidarCreacion(CrearPagoRequest request)
    {
        var errores = new List<string>();
        if (request.RES_id == Guid.Empty) errores.Add("El ID de la reserva es obligatorio.");
        if (request.PAG_monto <= 0) errores.Add("El monto del pago debe ser mayor a cero.");
        if (string.IsNullOrWhiteSpace(request.PAG_metodo)) errores.Add("El método de pago es obligatorio.");
        return errores;
    }
}