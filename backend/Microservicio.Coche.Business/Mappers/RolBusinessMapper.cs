using Microservicios.Coche.Business.DTOs.Rol;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class RolBusinessMapper
{
    public static RolDataModel ToDataModel(CrearRolRequest request)
    {
        return new RolDataModel
        {
            ROL_nombre = request.ROL_nombre,
            ROL_usuarioCreacion = request.ROL_usuarioCreacion,
            ROL_fechaCreacion = DateTime.UtcNow
        };
    }

    public static RolDataModel ToDataModel(ActualizarRolRequest request)
    {
        return new RolDataModel
        {
            ROL_id = request.ROL_id,
            ROL_nombre = request.ROL_nombre,
            ROL_usuarioModificacion = request.ROL_usuarioModificacion,
            ROL_fechaModificacion = DateTime.UtcNow
        };
    }

    public static RolResponse ToResponse(RolDataModel model)
    {
        return new RolResponse
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