using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class VehiculoDataMapper
{
    public static VehiculoDataModel ToDataModel(VehiculoEntity entity)
    {
        return new VehiculoDataModel
        {
            VEH_id = entity.VEH_id,
            CAT_id = entity.CAT_id,
            SUC_id = entity.SUC_id,
            VEH_placa = entity.VEH_placa,
            VEH_modelo = entity.VEH_modelo,
            VEH_anio = entity.VEH_anio,
            VEH_color = entity.VEH_color,
            VEH_kilometraje = entity.VEH_kilometraje,
            VEH_estado = entity.VEH_estado,
            VEH_fechaCreacion = entity.VEH_fechaCreacion,
            VEH_usuarioCreacion = entity.VEH_usuarioCreacion,
            VEH_fechaModificacion = entity.VEH_fechaModificacion,
            VEH_usuarioModificacion = entity.VEH_usuarioModificacion
        };
    }

    public static VehiculoEntity ToEntity(VehiculoDataModel model)
    {
        return new VehiculoEntity
        {
            VEH_id = model.VEH_id,
            CAT_id = model.CAT_id,
            SUC_id = model.SUC_id,
            VEH_placa = model.VEH_placa,
            VEH_modelo = model.VEH_modelo,
            VEH_anio = model.VEH_anio,
            VEH_color = model.VEH_color,
            VEH_kilometraje = model.VEH_kilometraje,
            VEH_estado = model.VEH_estado,
            VEH_fechaCreacion = model.VEH_fechaCreacion,
            VEH_usuarioCreacion = model.VEH_usuarioCreacion,
            VEH_fechaModificacion = model.VEH_fechaModificacion,
            VEH_usuarioModificacion = model.VEH_usuarioModificacion
        };
    }
}