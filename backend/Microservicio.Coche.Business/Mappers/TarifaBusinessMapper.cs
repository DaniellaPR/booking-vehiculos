
using Microservicios.Coche.Business.DTOs.Tarifa;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class TarifaBusinessMapper
{
    public static TarifaDataModel ToDataModel(CrearTarifaRequest request)
    {
        return new TarifaDataModel
        {
            CAT_id = request.CAT_id,
            TAR_precioDiario = request.TAR_precioDiario,
            TAR_usuarioCreacion = request.TAR_usuarioCreacion,
            TAR_fechaCreacion = DateTime.UtcNow
        };
    }

    public static TarifaDataModel ToDataModel(ActualizarTarifaRequest request)
    {
        return new TarifaDataModel
        {
            TAR_id = request.TAR_id,
            CAT_id = request.CAT_id,
            TAR_precioDiario = request.TAR_precioDiario,
            TAR_usuarioModificacion = request.TAR_usuarioModificacion,
            TAR_fechaModificacion = DateTime.UtcNow
        };
    }

    public static TarifaResponse ToResponse(TarifaDataModel model)
    {
        return new TarifaResponse
        {
            TAR_id = model.TAR_id,
            CAT_id = model.CAT_id,
            TAR_precioDiario = model.TAR_precioDiario,
            TAR_fechaCreacion = model.TAR_fechaCreacion,
            TAR_usuarioCreacion = model.TAR_usuarioCreacion,
            TAR_fechaModificacion = model.TAR_fechaModificacion,
            TAR_usuarioModificacion = model.TAR_usuarioModificacion
        };
    }
}