using Microservicios.Coche.Business.DTOs.Sucursal;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class SucursalMapper
{
    public static SucursalResponse ToResponse(this SucursalDataModel model)
    {
        if (model == null) return null!;
        return new SucursalResponse
        {
            SUC_id = model.SUC_id,
            SUC_nombre = model.SUC_nombre,
            SUC_ciudad = model.SUC_ciudad,
            SUC_direccion = model.SUC_direccion,
            SUC_coordenadas = model.SUC_coordenadas
        };
    }

    public static SucursalDataModel ToDataModel(this CrearSucursalRequest dto)
    {
        if (dto == null) return null!;
        return new SucursalDataModel
        {
            SUC_nombre = dto.SUC_nombre,
            SUC_ciudad = dto.SUC_ciudad,
            SUC_direccion = dto.SUC_direccion,
            SUC_coordenadas = dto.SUC_coordenadas,
            SUC_usuarioCreacion = dto.CreadoPorUsuario
        };
    }
}