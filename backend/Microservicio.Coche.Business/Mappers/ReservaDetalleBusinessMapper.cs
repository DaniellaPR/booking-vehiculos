using Microservicios.Coche.Business.DTOs.ReservaDetalle;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class ReservaDetalleBusinessMapper
{
    public static ReservaDetalleDataModel ToDataModel(CrearReservaDetalleRequest request)
    {
        return new ReservaDetalleDataModel
        {
            RES_id = request.RES_id,
            SEG_id = request.SEG_id,
            EXT_id = request.EXT_id,
            REX_cantidad = request.REX_cantidad,
            REX_usuarioCreacion = request.REX_usuarioCreacion,
            REX_fechaCreacion = DateTime.UtcNow
        };
    }

    public static ReservaDetalleDataModel ToDataModel(ActualizarReservaDetalleRequest request)
    {
        return new ReservaDetalleDataModel
        {
            REX_id = request.REX_id,
            RES_id = request.RES_id,
            SEG_id = request.SEG_id,
            EXT_id = request.EXT_id,
            REX_cantidad = request.REX_cantidad,
            REX_usuarioModificacion = request.REX_usuarioModificacion,
            REX_fechaModificacion = DateTime.UtcNow
        };
    }

    public static ReservaDetalleResponse ToResponse(ReservaDetalleDataModel model)
    {
        return new ReservaDetalleResponse
        {
            REX_id = model.REX_id,
            RES_id = model.RES_id,
            SEG_id = model.SEG_id,
            EXT_id = model.EXT_id,
            REX_cantidad = model.REX_cantidad ?? 0,
            REX_fechaCreacion = model.REX_fechaCreacion,
            REX_usuarioCreacion = model.REX_usuarioCreacion,
            REX_fechaModificacion = model.REX_fechaModificacion,
            REX_usuarioModificacion = model.REX_usuarioModificacion
        };
    }
}
