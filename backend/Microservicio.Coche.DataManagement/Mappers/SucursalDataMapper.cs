using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class SucursalDataMapper
{
    public static SucursalDataModel ToDataModel(this SucursalEntity entity)
    {
        if (entity == null) return null!;
        return new SucursalDataModel
        {
            SUC_id = entity.SUC_id,
            SUC_nombre = entity.SUC_nombre,
            SUC_ciudad = entity.SUC_ciudad,
            SUC_direccion = entity.SUC_direccion,
            SUC_coordenadas = entity.SUC_coordenadas
        };
    }

    public static SucursalEntity ToEntity(this SucursalDataModel model)
    {
        if (model == null) return null!;
        return new SucursalEntity
        {
            SUC_id = model.SUC_id,
            SUC_nombre = model.SUC_nombre,
            SUC_ciudad = model.SUC_ciudad,
            SUC_direccion = model.SUC_direccion,
            SUC_coordenadas = model.SUC_coordenadas
        };
    }
}