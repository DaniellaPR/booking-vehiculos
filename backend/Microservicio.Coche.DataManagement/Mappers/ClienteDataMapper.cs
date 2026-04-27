using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.DataManagement.Mappers;

public static class ClienteDataMapper
{
    public static ClienteDataModel ToDataModel(ClienteEntity entity)
    {
        return new ClienteDataModel
        {
            CLI_id = entity.CLI_id,
            CLI_nombres = entity.CLI_nombres,
            CLI_apellidos = entity.CLI_apellidos,
            CLI_cedula = entity.CLI_cedula,
            CLI_telefono = entity.CLI_telefono,
            CLI_fechaCreacion = entity.CLI_fechaCreacion,
            CLI_usuarioCreacion = entity.CLI_usuarioCreacion,
            CLI_fechaModificacion = entity.CLI_fechaModificacion,
            CLI_usuarioModificacion = entity.CLI_usuarioModificacion
        };
    }

    public static ClienteEntity ToEntity(ClienteDataModel model)
    {
        return new ClienteEntity
        {
            CLI_id = model.CLI_id,
            CLI_nombres = model.CLI_nombres,
            CLI_apellidos = model.CLI_apellidos,
            CLI_cedula = model.CLI_cedula,
            CLI_telefono = model.CLI_telefono,
            CLI_fechaCreacion = model.CLI_fechaCreacion,
            CLI_usuarioCreacion = model.CLI_usuarioCreacion,
            CLI_fechaModificacion = model.CLI_fechaModificacion,
            CLI_usuarioModificacion = model.CLI_usuarioModificacion
        };
    }
}