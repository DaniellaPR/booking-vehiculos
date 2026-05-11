using Microservicios.Coche.Business.DTOs.Reserva;
using Microservicios.Coche.Business.Exceptions;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Mappers;
using Microservicios.Coche.Business.Validators;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.Business.Services;

public class ReservaService : IReservaService
{
    private readonly IReservaDataService _reservaDataService;
    private readonly IClienteDataService _clienteDataService;
    private readonly ISucursalDataService _sucursalDataService;

    // ✅ NUEVO: Necesitamos acceder a los vehículos para validar stock y cambiar estados
    private readonly IVehiculoDataService _vehiculoDataService;

    public ReservaService(
        IReservaDataService reservaDataService,
        IClienteDataService clienteDataService,
        ISucursalDataService sucursalDataService,
        IVehiculoDataService vehiculoDataService)
    {
        _reservaDataService = reservaDataService;
        _clienteDataService = clienteDataService;
        _sucursalDataService = sucursalDataService;
        _vehiculoDataService = vehiculoDataService;
    }

    // ====================================================================
    // MÉTODOS DE TU DASHBOARD (Mantenemos tu código original intacto)
    // ====================================================================

    public async Task<ReservaResponse> CrearAsync(CrearReservaRequest request, CancellationToken cancellationToken = default)
    {
        // ... (Tu código original de CrearAsync)
        var errors = ReservaValidator.ValidarCreacion(request);
        if (errors.Any()) throw new ValidationException("La solicitud es inválida.", errors);

        var dataModel = ReservaBusinessMapper.ToDataModel(request);
        var creado = await _reservaDataService.CrearAsync(dataModel, cancellationToken);
        return ReservaBusinessMapper.ToResponse(creado);
    }

    public async Task<ReservaResponse> ActualizarAsync(ActualizarReservaRequest request, CancellationToken cancellationToken = default)
    {
        // ... (Tu código original de ActualizarAsync)
        var existente = await _reservaDataService.ObtenerPorIdAsync(request.RES_id, cancellationToken);
        if (existente is null) throw new NotFoundException("No se encontró la reserva.");

        var dataModel = ReservaBusinessMapper.ToDataModel(request);
        dataModel.RES_fechaCreacion = existente.RES_fechaCreacion;
        dataModel.RES_usuarioCreacion = existente.RES_usuarioCreacion;

        var actualizado = await _reservaDataService.ActualizarAsync(dataModel, cancellationToken);
        return ReservaBusinessMapper.ToResponse(actualizado);
    }

    public async Task<ReservaResponse> ObtenerPorIdAsync(Guid reservaId, CancellationToken cancellationToken = default)
    {
        var reserva = await _reservaDataService.ObtenerPorIdAsync(reservaId, cancellationToken);
        if (reserva is null) throw new NotFoundException("No se encontró la reserva.");
        return ReservaBusinessMapper.ToResponse(reserva);
    }

    public async Task<IReadOnlyList<ReservaResponse>> ListarAsync(CancellationToken cancellationToken = default)
    {
        var reservas = await _reservaDataService.ListarAsync(cancellationToken);
        return reservas.Select(ReservaBusinessMapper.ToResponse).ToList();
    }

    // ====================================================================
    // NUEVOS MÉTODOS DEL CONTRATO YAML (Lógica de Negocio Real)
    // ====================================================================

    public async Task<ReservaResponseDto> CrearReservaAsync(ReservaRequestDto request, CancellationToken cancellationToken = default)
    {
        // 1. Validar existencia de foráneas
        var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(request.VehiculoId, cancellationToken);
        if (vehiculo is null) throw new NotFoundException("El vehículo no existe.");

        // 2. REGLA DE NEGOCIO: VALIDACIÓN DE SOLAPAMIENTO DE FECHAS (STOCK)
        // Traemos las reservas (idealmente esto se hace con un query directo a BD por rendimiento, 
        // pero lo resolvemos con LINQ para cumplir la lógica rápidamente)
        var todasLasReservas = await _reservaDataService.ListarAsync(cancellationToken);

        var haySolapamiento = todasLasReservas.Any(r =>
            r.VEH_id == request.VehiculoId &&
            r.RES_estado != "Cancelada" &&
            r.RES_fechaRetiro < request.FechaEntrega &&
            r.RES_fechaEntrega > request.FechaRetiro);

        if (haySolapamiento)
        {
            // Lanzamos una excepción de negocio que tu Middleware atrapará (Error 400/409)
            throw new BusinessException("Conflicto: El vehículo ya se encuentra reservado en las fechas seleccionadas.");
        }

        // 3. Crear la reserva
        var nuevaReserva = new DataManagement.Models.ReservaDataModel
        {
            VEH_id = request.VehiculoId,
            CLI_id = request.ClienteId,
            RES_sucursalRetiroId = request.SucursalRetiroId,
            RES_sucursalEntregaId = request.SucursalEntregaId,
            RES_fechaRetiro = request.FechaRetiro,
            RES_fechaEntrega = request.FechaEntrega,
            RES_estado = "Pendiente",
            RES_fechaCreacion = DateTime.UtcNow,
            RES_usuarioCreacion = "api_booking"
        };

        var creado = await _reservaDataService.CrearAsync(nuevaReserva, cancellationToken);

        // 4. Retornar el DTO limpio que exige el Booking
        return new ReservaResponseDto
        {
            Id = creado.RES_id,
            VehiculoId = creado.VEH_id,
            // Agrega aquí los demás campos que tengas en tu ReservaResponseDto
        };
    }

    public async Task IniciarAlquilerAsync(Guid reservaId, int kmSalida, CancellationToken cancellationToken = default)
    {
        var reserva = await _reservaDataService.ObtenerPorIdAsync(reservaId, cancellationToken);
        if (reserva is null) throw new NotFoundException("Reserva no encontrada.");

        if (reserva.RES_estado != "Pendiente")
            throw new BusinessException("Solo se pueden iniciar alquileres en estado 'Pendiente'.");

        // Cambiar estado de la reserva
        reserva.RES_estado = "En Curso";
        await _reservaDataService.ActualizarAsync(reserva, cancellationToken);

        // Cambiar estado del vehículo
        var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(reserva.VEH_id, cancellationToken);
        if (vehiculo != null)
        {
            vehiculo.VEH_estado = "Alquilado";
            vehiculo.VEH_kilometraje = kmSalida; // Actualizamos el KM actual
            await _vehiculoDataService.ActualizarAsync(vehiculo, cancellationToken);
        }
    }

    public async Task RegistrarDevolucionAsync(Guid reservaId, int kmEntrada, decimal cargoExtra, CancellationToken cancellationToken = default)
    {
        var reserva = await _reservaDataService.ObtenerPorIdAsync(reservaId, cancellationToken);
        if (reserva is null) throw new NotFoundException("Reserva no encontrada.");

        if (reserva.RES_estado != "En Curso")
            throw new BusinessException("Solo se pueden devolver reservas que estén 'En Curso'.");

        // Cambiar estado de la reserva
        reserva.RES_estado = "Finalizada";
        await _reservaDataService.ActualizarAsync(reserva, cancellationToken);

        // Liberar el vehículo
        var vehiculo = await _vehiculoDataService.ObtenerPorIdAsync(reserva.VEH_id, cancellationToken);
        if (vehiculo != null)
        {
            vehiculo.VEH_estado = "Disponible";
            vehiculo.VEH_kilometraje = kmEntrada; // Registramos el nuevo KM
            await _vehiculoDataService.ActualizarAsync(vehiculo, cancellationToken);
        }

        // Aquí más adelante podemos llamar al Microservicio de Pagos para cobrar el cargoExtra
    }
}