using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class TarifaDataMapper
{
    public static TarifaDataModel ToDataModel(TarifaEntity entity)
    {
        return new TarifaDataModel
        {
            TAR_id = entity.TAR_id,
            CAT_id = entity.CAT_id,
            TAR_precioDiario = entity.TAR_precioDiario,
            TAR_fechaCreacion = entity.TAR_fechaCreacion,
            TAR_usuarioCreacion = entity.TAR_usuarioCreacion,
            TAR_fechaModificacion = entity.TAR_fechaModificacion,
            TAR_usuarioModificacion = entity.TAR_usuarioModificacion
        };
    }

    public static TarifaEntity ToEntity(TarifaDataModel model)
    {
        return new TarifaEntity
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