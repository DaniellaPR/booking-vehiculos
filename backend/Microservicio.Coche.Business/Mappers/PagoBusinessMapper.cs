using Microservicios.Coche.Business.DTOs.Pago;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class PagoBusinessMapper
{
    public static PagoDataModel ToDataModel(CrearPagoRequest request)
    {
        return new PagoDataModel
        {
            RES_id = request.RES_id,
            PAG_monto = request.PAG_monto,
            PAG_metodo = request.PAG_metodo,
            PAG_estado = request.PAG_estado,
            PAG_fechaPago = DateTime.UtcNow
        };
    }

    public static PagoResponse ToResponse(PagoDataModel model)
    {
        return new PagoResponse
        {
            PAG_id = model.PAG_id,
            RES_id = model.RES_id,
            PAG_monto = model.PAG_monto,
            PAG_metodo = model.PAG_metodo,
            PAG_estado = model.PAG_estado,
            PAG_fechaPago = model.PAG_fechaPago
        };
    }
}