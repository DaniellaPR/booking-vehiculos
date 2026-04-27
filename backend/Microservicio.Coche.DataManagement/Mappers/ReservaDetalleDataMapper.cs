using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class ReservaDetalleDataMapper
{
    public static ReservaDetalleDataModel ToDataModel(ReservaDetalleEntity entity)
    {
        return new ReservaDetalleDataModel
        {
            REX_id = entity.REX_id,
            RES_id = entity.RES_id,
            SEG_id = entity.SEG_id,
            EXT_id = entity.EXT_id,
            REX_cantidad = entity.REX_cantidad,
            REX_fechaCreacion = entity.REX_fechaCreacion,
            REX_usuarioCreacion = entity.REX_usuarioCreacion,
            REX_fechaModificacion = entity.REX_fechaModificacion,
            REX_usuarioModificacion = entity.REX_usuarioModificacion
        };
    }

    public static ReservaDetalleEntity ToEntity(ReservaDetalleDataModel model)
    {
        return new ReservaDetalleEntity
        {
            REX_id = model.REX_id,
            RES_id = model.RES_id,
            SEG_id = model.SEG_id,
            EXT_id = model.EXT_id,
            REX_cantidad = model.REX_cantidad,
            REX_fechaCreacion = model.REX_fechaCreacion,
            REX_usuarioCreacion = model.REX_usuarioCreacion,
            REX_fechaModificacion = model.REX_fechaModificacion,
            REX_usuarioModificacion = model.REX_usuarioModificacion
        };
    }
}