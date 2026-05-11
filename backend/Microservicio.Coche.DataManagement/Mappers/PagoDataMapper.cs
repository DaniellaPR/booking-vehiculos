using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class PagoDataMapper
{
    public static PagoDataModel ToDataModel(PagoEntity entity)
    {
        if (entity == null) return null!;
        return new PagoDataModel
        {
            PAG_id = entity.PAG_id,
            RES_id = entity.RES_id,
            PAG_monto = entity.PAG_monto,
            PAG_metodo = entity.PAG_metodo,
            PAG_estado = entity.PAG_estado,
            PAG_fechaPago = entity.PAG_fechaPago
        };
    }

    public static PagoEntity ToEntity(PagoDataModel model)
    {
        if (model == null) return null!;
        return new PagoEntity
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