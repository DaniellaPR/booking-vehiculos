using Microservicios.Coche.Business.DTOs.Vehiculo;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class VehiculoBusinessMapper
{
    public static VehiculoDataModel ToDataModel(CrearVehiculoRequest request)
    {
        return new VehiculoDataModel
        {
            CAT_id = request.CAT_id,
            SUC_id = request.SUC_id,
            VEH_placa = request.VEH_placa,
            VEH_modelo = request.VEH_modelo,
            VEH_anio = request.VEH_anio,
            VEH_color = request.VEH_color,
            VEH_kilometraje = request.VEH_kilometraje,
            VEH_estado = request.VEH_estado,
            VEH_usuarioCreacion = request.VEH_usuarioCreacion,
            VEH_fechaCreacion = DateTime.UtcNow
        };
    }

    public static VehiculoDataModel ToDataModel(ActualizarVehiculoRequest request)
    {
        return new VehiculoDataModel
        {
            VEH_id = request.VEH_id,
            CAT_id = request.CAT_id,
            SUC_id = request.SUC_id,
            VEH_placa = request.VEH_placa,
            VEH_modelo = request.VEH_modelo,
            VEH_anio = request.VEH_anio,
            VEH_color = request.VEH_color,
            VEH_kilometraje = request.VEH_kilometraje,
            VEH_estado = request.VEH_estado,
            VEH_usuarioModificacion = request.VEH_usuarioModificacion,
            VEH_fechaModificacion = DateTime.UtcNow
        };
    }

    public static VehiculoResponse ToResponse(VehiculoDataModel model)
    {
        return new VehiculoResponse
        {
            VEH_id = model.VEH_id,
            CAT_id = model.CAT_id,
            SUC_id = model.SUC_id,
            VEH_placa = model.VEH_placa,
            VEH_modelo = model.VEH_modelo,
            VEH_anio = model.VEH_anio,
            VEH_color = model.VEH_color,
            VEH_kilometraje = model.VEH_kilometraje ?? 0m,
            VEH_estado = model.VEH_estado ?? string.Empty,
            VEH_fechaCreacion = model.VEH_fechaCreacion,
            VEH_usuarioCreacion = model.VEH_usuarioCreacion,
            VEH_fechaModificacion = model.VEH_fechaModificacion,
            VEH_usuarioModificacion = model.VEH_usuarioModificacion
        };
    }
}
