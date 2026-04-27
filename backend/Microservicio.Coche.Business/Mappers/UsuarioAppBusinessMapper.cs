
using Microservicios.Coche.Business.DTOs.UsuarioApp;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class UsuarioAppBusinessMapper
{
    public static UsuarioAppDataModel ToDataModel(CrearUsuarioAppRequest request)
    {
        return new UsuarioAppDataModel
        {
            ROL_id = request.ROL_id,
            USU_email = request.USU_email,
            USU_passwordHash = request.USU_passwordHash,
            USU_usuarioCreacion = request.USU_usuarioCreacion,
            USU_fechaCreacion = DateTime.UtcNow
        };
    }

    public static UsuarioAppDataModel ToDataModel(ActualizarUsuarioAppRequest request)
    {
        return new UsuarioAppDataModel
        {
            USU_id = request.USU_id,
            ROL_id = request.ROL_id,
            USU_email = request.USU_email,
            USU_passwordHash = request.USU_passwordHash,
            USU_usuarioModificacion = request.USU_usuarioModificacion,
            USU_fechaModificacion = DateTime.UtcNow
        };
    }

    public static UsuarioAppResponse ToResponse(UsuarioAppDataModel model)
    {
        return new UsuarioAppResponse
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