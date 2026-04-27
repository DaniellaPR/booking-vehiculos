
using Microservicios.Coche.Business.DTOs.HorarioAtencion;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class HorarioAtencionBusinessMapper
{
    public static HorarioAtencionDataModel ToDataModel(CrearHorarioAtencionRequest request)
    {
        return new HorarioAtencionDataModel
        {
            SUC_id = request.SUC_id,
            HOR_diaSemana = request.HOR_diaSemana,
            HOR_apertura = request.HOR_apertura,
            HOR_cierre = request.HOR_cierre,
            HOR_usuarioCreacion = request.HOR_usuarioCreacion,
            HOR_fechaCreacion = DateTime.UtcNow
        };
    }

    public static HorarioAtencionDataModel ToDataModel(ActualizarHorarioAtencionRequest request)
    {
        return new HorarioAtencionDataModel
        {
            HOR_id = request.HOR_id,
            SUC_id = request.SUC_id,
            HOR_diaSemana = request.HOR_diaSemana,
            HOR_apertura = request.HOR_apertura,
            HOR_cierre = request.HOR_cierre,
            HOR_usuarioModificacion = request.HOR_usuarioModificacion,
            HOR_fechaModificacion = DateTime.UtcNow
        };
    }

    public static HorarioAtencionResponse ToResponse(HorarioAtencionDataModel model)
    {
        return new HorarioAtencionResponse
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