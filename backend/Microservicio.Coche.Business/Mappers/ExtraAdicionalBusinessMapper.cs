using Microservicios.Coche.Business.DTOs.ExtraAdicional;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class ExtraAdicionalMapper
{
    public static ExtraAdicionalResponse ToDto(this ExtraAdicionalDataModel model)
    {
        if (model == null) return null!;
        return new ExtraAdicionalResponse
        {
            EXT_id = model.EXT_id,
            EXT_nombre = model.EXT_nombre,
            EXT_costo = model.EXT_costo
        };
    }

    public static ExtraAdicionalDataModel ToDataModel(this CrearExtraAdicionalRequest dto)
    {
        if (dto == null) return null!;
        return new ExtraAdicionalDataModel
        {
            EXT_nombre = dto.EXT_nombre,
            EXT_costo = dto.EXT_costo
        };
    }

    public static ExtraAdicionalDataModel ToDataModel(this ActualizarExtraAdicionalRequest dto, ExtraAdicionalDataModel existente)
    {
        if (dto == null) return null!;
        existente.EXT_nombre = dto.EXT_nombre;
        existente.EXT_costo = dto.EXT_costo;
        return existente;
    }
}