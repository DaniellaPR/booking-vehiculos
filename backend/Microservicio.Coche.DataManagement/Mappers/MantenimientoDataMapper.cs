using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class MantenimientoDataMapper
{
    public static MantenimientoDataModel ToDataModel(MantenimientoEntity entity)
    {
        return new MantenimientoDataModel
        {
            MAN_id = entity.MAN_id,
            VEH_id = entity.VEH_id,
            MAN_fecha = entity.MAN_fecha,
            MAN_descripcion = entity.MAN_descripcion,
            MAN_costo = entity.MAN_costo,
            MAN_fechaCreacion = entity.MAN_fechaCreacion,
            MAN_usuarioCreacion = entity.MAN_usuarioCreacion,
            MAN_fechaModificacion = entity.MAN_fechaModificacion,
            MAN_usuarioModificacion = entity.MAN_usuarioModificacion
        };
    }

    public static MantenimientoEntity ToEntity(MantenimientoDataModel model)
    {
        return new MantenimientoEntity
        {
            MAN_id = model.MAN_id,
            VEH_id = model.VEH_id,
            MAN_fecha = model.MAN_fecha,
            MAN_descripcion = model.MAN_descripcion,
            MAN_costo = model.MAN_costo,
            MAN_fechaCreacion = model.MAN_fechaCreacion,
            MAN_usuarioCreacion = model.MAN_usuarioCreacion,
            MAN_fechaModificacion = model.MAN_fechaModificacion,
            MAN_usuarioModificacion = model.MAN_usuarioModificacion
        };
    }
}