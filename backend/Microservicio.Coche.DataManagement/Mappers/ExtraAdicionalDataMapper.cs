using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class ExtraAdicionalDataMapper
{
    public static ExtraAdicionalDataModel ToDataModel(ExtraAdicionalEntity entity)
    {
        if (entity == null) return null!;
        return new ExtraAdicionalDataModel
        {
            EXT_id = entity.EXT_id,
            EXT_nombre = entity.EXT_nombre,
            EXT_costo = entity.EXT_costo,
            EXT_fechaCreacion = entity.EXT_fechaCreacion,
            EXT_usuarioCreacion = entity.EXT_usuarioCreacion,
            EXT_fechaModificacion = entity.EXT_fechaModificacion,
            EXT_usuarioModificacion = entity.EXT_usuarioModificacion
        };
    }

    public static ExtraAdicionalEntity ToEntity(ExtraAdicionalDataModel model)
    {
        if (model == null) return null!;
        return new ExtraAdicionalEntity
        {
            EXT_id = model.EXT_id,
            EXT_nombre = model.EXT_nombre,
            EXT_costo = model.EXT_costo,
            EXT_fechaCreacion = model.EXT_fechaCreacion,
            EXT_usuarioCreacion = model.EXT_usuarioCreacion,
            EXT_fechaModificacion = model.EXT_fechaModificacion,
            EXT_usuarioModificacion = model.EXT_usuarioModificacion
        };
    }
}