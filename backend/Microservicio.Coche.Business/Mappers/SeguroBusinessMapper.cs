using Microservicios.Coche.Business.DTOs.Seguro;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class SeguroMapper
{
    // 🚨 Asegúrate de que este método se llame ToResponse
    public static SeguroResponse ToResponse(this SeguroDataModel model)
    {
        if (model == null) return null!;
        return new SeguroResponse
        {
            SEG_id = model.SEG_id,
            SEG_nombre = model.SEG_nombre,
            SEG_costoDiario = model.SEG_costoDiario
        };
    }

    public static SeguroDataModel ToDataModel(this CrearSeguroRequest dto)
    {
        if (dto == null) return null!;
        return new SeguroDataModel
        {
            SEG_nombre = dto.SEG_nombre,
            SEG_costoDiario = dto.SEG_costoDiario,
            SEG_usuarioCreacion = dto.SEG_usuarioCreacion
        };
    }
}