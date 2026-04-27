using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class RolDataMapper
{
    public static RolDataModel ToDataModel(RolEntity entity)
    {
        return new RolDataModel
        {
            ROL_id = entity.ROL_id,
            ROL_nombre = entity.ROL_nombre,
            ROL_fechaCreacion = entity.ROL_fechaCreacion,
            ROL_usuarioCreacion = entity.ROL_usuarioCreacion,
            ROL_fechaModificacion = entity.ROL_fechaModificacion,
            ROL_usuarioModificacion = entity.ROL_usuarioModificacion
        };
    }

    public static RolEntity ToEntity(RolDataModel model)
    {
        return new RolEntity
        {
            ROL_id = model.ROL_id,
            ROL_nombre = model.ROL_nombre,
            ROL_fechaCreacion = model.ROL_fechaCreacion,
            ROL_usuarioCreacion = model.ROL_usuarioCreacion,
            ROL_fechaModificacion = model.ROL_fechaModificacion,
            ROL_usuarioModificacion = model.ROL_usuarioModificacion
        };
    }
}
