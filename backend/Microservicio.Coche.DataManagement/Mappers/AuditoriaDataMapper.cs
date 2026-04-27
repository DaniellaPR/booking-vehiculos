using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class AuditoriaDataMapper
{
    public static AuditoriaDataModel ToDataModel(AuditoriaEntity entity)
    {
        return new AuditoriaDataModel
        {
            AUD_id = entity.AUD_id,
            AUD_nombreTabla = entity.AUD_nombreTabla,
            AUD_operacion = entity.AUD_operacion,
            AUD_usuario = entity.AUD_usuario,
            AUD_fecha = entity.AUD_fecha,
            AUD_detalleJsonb = entity.AUD_detalleJsonb
        };
    }

    public static AuditoriaEntity ToEntity(AuditoriaDataModel model)
    {
        return new AuditoriaEntity
        {
            AUD_id = model.AUD_id,
            AUD_nombreTabla = model.AUD_nombreTabla,
            AUD_operacion = model.AUD_operacion,
            AUD_usuario = model.AUD_usuario,
            AUD_fecha = model.AUD_fecha,
            AUD_detalleJsonb = model.AUD_detalleJsonb
        };
    }
}
