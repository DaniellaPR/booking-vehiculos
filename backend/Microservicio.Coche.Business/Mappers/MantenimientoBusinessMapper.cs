
using Microservicios.Coche.Business.DTOs.Mantenimiento;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class MantenimientoBusinessMapper
{
    public static MantenimientoDataModel ToDataModel(CrearMantenimientoRequest request)
    {
        return new MantenimientoDataModel
        {
            VEH_id = request.VEH_id,
            MAN_fecha = request.MAN_fecha.ToUniversalTime(),
            MAN_descripcion = request.MAN_descripcion,
            MAN_costo = request.MAN_costo,
            MAN_usuarioCreacion = request.MAN_usuarioCreacion,
            MAN_fechaCreacion = DateTime.UtcNow
        };
    }

    public static MantenimientoDataModel ToDataModel(ActualizarMantenimientoRequest request)
    {
        return new MantenimientoDataModel
        {
            MAN_id = request.MAN_id,
            VEH_id = request.VEH_id,
            MAN_fecha = request.MAN_fecha.ToUniversalTime(),
            MAN_descripcion = request.MAN_descripcion,
            MAN_costo = request.MAN_costo,
            MAN_usuarioModificacion = request.MAN_usuarioModificacion,
            MAN_fechaModificacion = DateTime.UtcNow
        };
    }

    public static MantenimientoResponse ToResponse(MantenimientoDataModel model)
    {
        return new MantenimientoResponse
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