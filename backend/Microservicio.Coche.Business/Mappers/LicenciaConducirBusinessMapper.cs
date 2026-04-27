using Microservicios.Coche.Business.DTOs.LicenciaConducir;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class LicenciaConducirBusinessMapper
{
    public static LicenciaConducirDataModel ToDataModel(CrearLicenciaConducirRequest request)
    {
        return new LicenciaConducirDataModel
        {
            CLI_id = request.CLI_id,
            LIC_numero = request.LIC_numero,
            LIC_categoria = request.LIC_categoria,
            LIC_vigencia = request.LIC_vigencia.ToUniversalTime(),
            LIC_usuarioCreacion = request.LIC_usuarioCreacion,
            LIC_fechaCreacion = DateTime.UtcNow
        };
    }

    public static LicenciaConducirDataModel ToDataModel(ActualizarLicenciaConducirRequest request)
    {
        return new LicenciaConducirDataModel
        {
            LIC_id = request.LIC_id,
            CLI_id = request.CLI_id,
            LIC_numero = request.LIC_numero,
            LIC_categoria = request.LIC_categoria,
            LIC_vigencia = request.LIC_vigencia.ToUniversalTime(),
            LIC_usuarioModificacion = request.LIC_usuarioModificacion,
            LIC_fechaModificacion = DateTime.UtcNow
        };
    }

    public static LicenciaConducirResponse ToResponse(LicenciaConducirDataModel model)
    {
        return new LicenciaConducirResponse
        {
            LIC_id = model.LIC_id,
            CLI_id = model.CLI_id,
            LIC_numero = model.LIC_numero,
            LIC_categoria = model.LIC_categoria,
            LIC_vigencia = model.LIC_vigencia,
            LIC_fechaCreacion = model.LIC_fechaCreacion,
            LIC_usuarioCreacion = model.LIC_usuarioCreacion,
            LIC_fechaModificacion = model.LIC_fechaModificacion,
            LIC_usuarioModificacion = model.LIC_usuarioModificacion
        };
    }
}