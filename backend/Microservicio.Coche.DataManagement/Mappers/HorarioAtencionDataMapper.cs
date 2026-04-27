using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class HorarioAtencionDataMapper
{
    public static HorarioAtencionDataModel ToDataModel(HorarioAtencionEntity entity)
    {
        return new HorarioAtencionDataModel
        {
            HOR_id = entity.HOR_id,
            SUC_id = entity.SUC_id,
            HOR_diaSemana = entity.HOR_diaSemana,
            HOR_apertura = entity.HOR_apertura,
            HOR_cierre = entity.HOR_cierre,
            HOR_fechaCreacion = entity.HOR_fechaCreacion,
            HOR_usuarioCreacion = entity.HOR_usuarioCreacion,
            HOR_fechaModificacion = entity.HOR_fechaModificacion,
            HOR_usuarioModificacion = entity.HOR_usuarioModificacion
        };
    }

    public static HorarioAtencionEntity ToEntity(HorarioAtencionDataModel model)
    {
        return new HorarioAtencionEntity
        {
            HOR_id = model.HOR_id,
            SUC_id = model.SUC_id,
            HOR_diaSemana = model.HOR_diaSemana,
            HOR_apertura = model.HOR_apertura,
            HOR_cierre = model.HOR_cierre,
            HOR_fechaCreacion = model.HOR_fechaCreacion,
            HOR_usuarioCreacion = model.HOR_usuarioCreacion,
            HOR_fechaModificacion = model.HOR_fechaModificacion,
            HOR_usuarioModificacion = model.HOR_usuarioModificacion
        };
    }
}