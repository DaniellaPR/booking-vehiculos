using Microservicios.Coche.Business.DTOs.Cliente;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Mappers;

public static class ClienteBusinessMapper
{
    public static ClienteDataModel ToDataModel(CrearClienteRequest request)
    {
        return new ClienteDataModel
        {
            CLI_nombres = request.CLI_nombres,
            CLI_apellidos = request.CLI_apellidos,
            CLI_cedula = request.CLI_cedula,
            CLI_telefono = request.CLI_telefono,
            CLI_usuarioCreacion = request.CLI_usuarioCreacion,
            CLI_fechaCreacion = DateTime.UtcNow
        };
    }

    public static ClienteDataModel ToDataModel(ActualizarClienteRequest request)
    {
        return new ClienteDataModel
        {
            CLI_id = request.CLI_id,
            CLI_nombres = request.CLI_nombres,
            CLI_apellidos = request.CLI_apellidos,
            CLI_cedula = request.CLI_cedula,
            CLI_telefono = request.CLI_telefono,
            CLI_usuarioModificacion = request.CLI_usuarioModificacion,
            CLI_fechaModificacion = DateTime.UtcNow
        };
    }

    public static ClienteResponse ToResponse(ClienteDataModel model)
    {
        return new ClienteResponse
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