using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class ReservaDataMapper
{
    public static ReservaDataModel ToDataModel(ReservaEntity entity)
    {
        return new ReservaDataModel
        {
            RES_id = entity.RES_id,
            CLI_id = entity.CLI_id,
            RES_sucursalRetiroId = entity.RES_sucursalRetiroId,
            RES_sucursalEntregaId = entity.RES_sucursalEntregaId,
            RES_fechaRetiro = entity.RES_fechaRetiro,
            RES_fechaEntrega = entity.RES_fechaEntrega,
            RES_estado = entity.RES_estado,
            RES_fechaCreacion = entity.RES_fechaCreacion,
            RES_usuarioCreacion = entity.RES_usuarioCreacion,
            RES_fechaModificacion = entity.RES_fechaModificacion,
            RES_usuarioModificacion = entity.RES_usuarioModificacion
        };
    }

    public static ReservaEntity ToEntity(ReservaDataModel model)
    {
        return new ReservaEntity
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