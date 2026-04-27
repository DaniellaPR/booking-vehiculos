using Microservicios.Coche.Business.DTOs.Auditoria;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class AuditoriaBusinessMapper
{
    public static AuditoriaDataModel ToDataModel(CrearAuditoriaRequest request)
    {
        return new AuditoriaDataModel
        {
            AUD_nombreTabla = request.AUD_nombreTabla,
            AUD_operacion = request.AUD_operacion,
            AUD_usuario = request.AUD_usuario,
            AUD_detalleJsonb = request.AUD_detalleJsonb,
            AUD_fecha = DateTime.UtcNow
        };
    }

    public static AuditoriaResponse ToResponse(AuditoriaDataModel model)
    {
        return new AuditoriaResponse
        {
            AUD_id = model.AUD_id,
            AUD_nombreTabla = model.AUD_nombreTabla,
            AUD_operacion = model.AUD_operacion,
            AUD_usuario = model.AUD_usuario,
            AUD_fecha = model.AUD_fecha,
            AUD_detalleJsonb = model.AUD_detalleJsonb
        };
    }
}