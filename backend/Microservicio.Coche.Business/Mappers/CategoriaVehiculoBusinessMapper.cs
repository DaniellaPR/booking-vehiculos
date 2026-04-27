using Microservicios.Coche.Business.DTOs.CategoriaVehiculo;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class CategoriaVehiculoMapper
{
    public static CategoriaVehiculoResponse ToDto(this CategoriaVehiculoDataModel model)
    {
        if (model == null) return null!;
        return new CategoriaVehiculoResponse
        {
            CAT_id = model.CAT_id,
            CAT_nombre = model.CAT_nombre,
            CAT_descripcion = model.CAT_descripcion,
            CAT_costoBase = model.CAT_costoBase
        };
    }

    public static CategoriaVehiculoDataModel ToDataModel(this CrearCategoriaVehiculoRequest dto)
    {
        if (dto == null) return null!;
        return new CategoriaVehiculoDataModel
        {
            CAT_nombre = dto.CAT_nombre,
            CAT_descripcion = dto.CAT_descripcion,
            CAT_costoBase = dto.CAT_costoBase
        };
    }

    public static CategoriaVehiculoDataModel ToDataModel(this ActualizarCategoriaVehiculoRequest dto, CategoriaVehiculoDataModel existente)
    {
        if (dto == null) return null!;
        existente.CAT_nombre = dto.CAT_nombre;
        existente.CAT_descripcion = dto.CAT_descripcion;
        existente.CAT_costoBase = dto.CAT_costoBase;
        return existente;
    }
}