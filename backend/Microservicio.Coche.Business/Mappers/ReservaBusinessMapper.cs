
using Microservicios.Coche.Business.DTOs.Reserva;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class ReservaBusinessMapper
{
    public static ReservaDataModel ToDataModel(CrearReservaRequest request)
    {
        return new ReservaDataModel
        {
            CLI_id = request.CLI_id,
            RES_sucursalRetiroId = request.RES_sucursalRetiroId,
            RES_sucursalEntregaId = request.RES_sucursalEntregaId,
            RES_fechaRetiro = request.RES_fechaRetiro.ToUniversalTime(),
            RES_fechaEntrega = request.RES_fechaEntrega.ToUniversalTime(),
            RES_estado = request.RES_estado,
            RES_usuarioCreacion = request.RES_usuarioCreacion,
            RES_fechaCreacion = DateTime.UtcNow
        };
    }

    public static ReservaDataModel ToDataModel(ActualizarReservaRequest request)
    {
        return new ReservaDataModel
        {
            RES_id = request.RES_id,
            CLI_id = request.CLI_id,
            RES_sucursalRetiroId = request.RES_sucursalRetiroId,
            RES_sucursalEntregaId = request.RES_sucursalEntregaId,
            RES_fechaRetiro = request.RES_fechaRetiro.ToUniversalTime(),
            RES_fechaEntrega = request.RES_fechaEntrega.ToUniversalTime(),
            RES_estado = request.RES_estado,
            RES_usuarioModificacion = request.RES_usuarioModificacion,
            RES_fechaModificacion = DateTime.UtcNow
        };
    }

    public static ReservaResponse ToResponse(ReservaDataModel model)
    {
        return new ReservaResponse
        {
            RES_id = model.RES_id,
            CLI_id = model.CLI_id,
            RES_sucursalRetiroId = model.RES_sucursalRetiroId,
            RES_sucursalEntregaId = model.RES_sucursalEntregaId,
            RES_fechaRetiro = model.RES_fechaRetiro,
            RES_fechaEntrega = model.RES_fechaEntrega,
            RES_estado = model.RES_estado,
            RES_fechaCreacion = model.RES_fechaCreacion,
            RES_usuarioCreacion = model.RES_usuarioCreacion,
            RES_fechaModificacion = model.RES_fechaModificacion,
            RES_usuarioModificacion = model.RES_usuarioModificacion
        };
    }
}