using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class LicenciaConducirDataMapper
{
    public static LicenciaConducirDataModel ToDataModel(LicenciaConducirEntity entity)
    {
        return new LicenciaConducirDataModel
        {
            LIC_id = entity.LIC_id,
            CLI_id = entity.CLI_id,
            LIC_numero = entity.LIC_numero,
            LIC_categoria = entity.LIC_categoria,
            LIC_vigencia = entity.LIC_vigencia,
            LIC_fechaCreacion = entity.LIC_fechaCreacion,
            LIC_usuarioCreacion = entity.LIC_usuarioCreacion,
            LIC_fechaModificacion = entity.LIC_fechaModificacion,
            LIC_usuarioModificacion = entity.LIC_usuarioModificacion
        };
    }

    public static LicenciaConducirEntity ToEntity(LicenciaConducirDataModel model)
    {
        return new LicenciaConducirEntity
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