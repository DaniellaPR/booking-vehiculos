using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class CategoriaVehiculoDataMapper
{
    public static CategoriaVehiculoDataModel ToDataModel(CategoriaVehiculoEntity entity)
    {
        if (entity == null) return null!;
        return new CategoriaVehiculoDataModel
        {
            CAT_id = entity.CAT_id,
            CAT_nombre = entity.CAT_nombre,
            CAT_descripcion = entity.CAT_descripcion,
            CAT_costoBase = entity.CAT_costoBase,
            CAT_capacidadPasajeros = entity.CAT_capacidadPasajeros,
            CAT_capacidadMaletas = entity.CAT_capacidadMaletas,
            CAT_tipoTransmision = entity.CAT_tipoTransmision,
            CAT_fechaCreacion = entity.CAT_fechaCreacion,
            CAT_usuarioCreacion = entity.CAT_usuarioCreacion,
            CAT_fechaModificacion = entity.CAT_fechaModificacion,
            CAT_usuarioModificacion = entity.CAT_usuarioModificacion
        };
    }

    public static CategoriaVehiculoEntity ToEntity(CategoriaVehiculoDataModel model)
    {
        if (model == null) return null!;
        return new CategoriaVehiculoEntity
        {
            CAT_id = model.CAT_id,
            CAT_nombre = model.CAT_nombre,
            CAT_descripcion = model.CAT_descripcion,
            CAT_costoBase = model.CAT_costoBase,
            CAT_capacidadPasajeros = model.CAT_capacidadPasajeros,
            CAT_capacidadMaletas = model.CAT_capacidadMaletas,
            CAT_tipoTransmision = model.CAT_tipoTransmision,
            CAT_fechaCreacion = model.CAT_fechaCreacion,
            CAT_usuarioCreacion = model.CAT_usuarioCreacion,
            CAT_fechaModificacion = model.CAT_fechaModificacion,
            CAT_usuarioModificacion = model.CAT_usuarioModificacion
        };
    }
}