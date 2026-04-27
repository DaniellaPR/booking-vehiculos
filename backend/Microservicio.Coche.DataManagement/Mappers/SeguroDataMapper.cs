using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class SeguroDataMapper
{
    public static SeguroDataModel ToDataModel(this SeguroEntity entity)
    {
        if (entity == null) return null!;
        return new SeguroDataModel
        {
            SEG_id = entity.SEG_id,
            SEG_nombre = entity.SEG_nombre,
            SEG_costoDiario = entity.SEG_costoDiario,
            SEG_fechaCreacion = entity.SEG_fechaCreacion,
            SEG_usuarioCreacion = entity.SEG_usuarioCreacion,
            SEG_fechaModificacion = entity.SEG_fechaModificacion,
            SEG_usuarioModificacion = entity.SEG_usuarioModificacion
        };
    }

    public static SeguroEntity ToEntity(this SeguroDataModel model)
    {
        if (model == null) return null!;
        return new SeguroEntity
        {
            SEG_id = model.SEG_id,
            SEG_nombre = model.SEG_nombre,
            SEG_costoDiario = model.SEG_costoDiario,
            SEG_fechaCreacion = model.SEG_fechaCreacion,
            SEG_usuarioCreacion = model.SEG_usuarioCreacion,
            SEG_fechaModificacion = model.SEG_fechaModificacion,
            SEG_usuarioModificacion = model.SEG_usuarioModificacion
        };
    }
}