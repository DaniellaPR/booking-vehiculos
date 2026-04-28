using Microservicios.Coche.Business.DTOs.Cliente;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.Services;

public class ClienteService : IClienteService
{
    private readonly IClienteDataService _clienteDataService;
    private readonly IUsuarioAppDataService _usuarioAppDataService;
    private readonly IRolDataService _rolDataService;
    private readonly IUnitOfWork _unitOfWork;

    // Se inyectan los nuevos servicios necesarios para crear el usuario en la BD
    public ClienteService(
        IClienteDataService clienteDataService,
        IUsuarioAppDataService usuarioAppDataService,
        IRolDataService rolDataService,
        IUnitOfWork unitOfWork)
    {
        _clienteDataService = clienteDataService;
        _usuarioAppDataService = usuarioAppDataService;
        _rolDataService = rolDataService;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<ClienteResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var clientes = await _clienteDataService.ListarAsync(cancellationToken);
        return clientes.Select(ClienteBusinessMapper.ToResponse).ToList();
    }

    public async Task<ClienteResponse> ObtenerPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var cliente = await _clienteDataService.ObtenerPorIdAsync(id, cancellationToken);
        if (cliente == null)
            throw new NotFoundException($"Cliente con ID {id} no encontrado");
        return ClienteBusinessMapper.ToResponse(cliente);
    }

    public async Task<ClienteResponse> CrearAsync(CrearClienteRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ClienteValidator.ValidarCreacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de creación de cliente es inválida.", errors);

        // 1. Validar que la cédula no exista
        var existenteCedula = await _clienteDataService.ObtenerPorCedulaAsync(request.CLI_cedula, cancellationToken);
        if (existenteCedula != null)
            throw new BusinessException($"Ya existe un cliente con cédula {request.CLI_cedula}.");

        // 2. Validar que el correo no esté siendo usado por otro usuario
        var existenteCorreo = await _usuarioAppDataService.ObtenerPorEmailAsync(request.CLI_correo, cancellationToken);
        if (existenteCorreo != null)
            throw new BusinessException($"El correo {request.CLI_correo} ya está registrado.");

        // 3. Crear Cliente
        var clienteDataModel = ClienteBusinessMapper.ToDataModel(request);
        clienteDataModel.CLI_id = Guid.NewGuid();

        var clienteCreado = await _clienteDataService.CrearAsync(clienteDataModel, cancellationToken);

        // 4. Crear Credenciales de Acceso (UsuarioApp)
        // Usamos ListarAsync y filtramos en memoria para evitar modificar la interfaz de Roles
        var roles = await _rolDataService.ListarAsync(cancellationToken);
        var rolCliente = roles.FirstOrDefault(r => r.ROL_nombre != null && r.ROL_nombre.ToUpper() == "CLIENTE");
        var rolId = rolCliente?.ROL_id ?? Guid.Empty;

        var nuevoUsuario = new UsuarioAppDataModel
        {
            USU_id = clienteCreado.CLI_id, // Usamos el mismo ID del cliente
            ROL_id = rolId,
            USU_email = request.CLI_correo,
            USU_passwordHash = request.Password, // Texto plano por ahora
            USU_fechaCreacion = DateTime.UtcNow,
            USU_usuarioCreacion = request.CLI_usuarioCreacion ?? "sistema_registro"
        };

        await _usuarioAppDataService.CrearAsync(nuevoUsuario, cancellationToken);

        // 5. Confirmar la transacción (Corregido a SaveChangesAsync)
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ClienteBusinessMapper.ToResponse(clienteCreado);
    }

    public async Task<ClienteResponse> ActualizarAsync(ActualizarClienteRequest request, CancellationToken cancellationToken = default)
    {
        var errors = ClienteValidator.ValidarActualizacion(request);
        if (errors.Any())
            throw new ValidationException("La solicitud de actualización de cliente es inválida.", errors);

        var existente = await _clienteDataService.ObtenerPorIdAsync(request.CLI_id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró el cliente solicitado.");

        var dataModel = ClienteBusinessMapper.ToDataModel(request);

        // Conservar campos de auditoría
        dataModel.CLI_fechaCreacion = existente.CLI_fechaCreacion;
        dataModel.CLI_usuarioCreacion = existente.CLI_usuarioCreacion;

        var actualizado = await _clienteDataService.ActualizarAsync(dataModel, cancellationToken);
        if (actualizado is null)
            throw new NotFoundException("No se pudo actualizar el cliente porque no existe.");

        return ClienteBusinessMapper.ToResponse(actualizado);
    }

    public async Task<bool> EliminarAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var existente = await _clienteDataService.ObtenerPorIdAsync(id, cancellationToken);
        if (existente is null)
            throw new NotFoundException("No se encontró el cliente a eliminar.");

        var eliminado = await _clienteDataService.EliminarAsync(id, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return eliminado;
    }
}