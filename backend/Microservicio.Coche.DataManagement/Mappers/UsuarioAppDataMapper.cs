using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class UsuarioAppDataMapper
{
    public static UsuarioAppDataModel ToDataModel(UsuarioAppEntity entity)
    {
        return new UsuarioAppDataModel
        {
            USU_id = entity.USU_id,
            ROL_id = entity.ROL_id,
            USU_email = entity.USU_email,
            USU_passwordHash = entity.USU_passwordHash,
            USU_fechaCreacion = entity.USU_fechaCreacion,
            USU_usuarioCreacion = entity.USU_usuarioCreacion,
            USU_fechaModificacion = entity.USU_fechaModificacion,
            USU_usuarioModificacion = entity.USU_usuarioModificacion
        };
    }

    public static UsuarioAppEntity ToEntity(UsuarioAppDataModel model)
    {
        return new UsuarioAppEntity
        {
            USU_id = model.USU_id,
            ROL_id = model.ROL_id,
            USU_email = model.USU_email,
            USU_passwordHash = model.USU_passwordHash,
            USU_fechaCreacion = model.USU_fechaCreacion,
            USU_usuarioCreacion = model.USU_usuarioCreacion,
            USU_fechaModificacion = model.USU_fechaModificacion,
            USU_usuarioModificacion = model.USU_usuarioModificacion
        };
    }
}
