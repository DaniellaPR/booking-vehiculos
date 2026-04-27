using Microservicios.Coche.DataAccess.Queries;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;

namespace Microservicios.Coche.DataManagement.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Módulo Infraestructura y Seguridad
    IAuditoriaRepository AuditoriaRepository { get; }
    AuditoriaQueryRepository AuditoriaQueryRepository { get; }

    IRolRepository RolRepository { get; }
    RolQueryRepository RolQueryRepository { get; }

    IUsuarioAppRepository UsuarioAppRepository { get; }
    UsuarioAppQueryRepository UsuarioAppQueryRepository { get; }

    // Módulo Clientes
    IClienteRepository ClienteRepository { get; }
    ClienteQueryRepository ClienteQueryRepository { get; }

    ILicenciaConducirRepository LicenciaConducirRepository { get; }
    LicenciaConducirQueryRepository LicenciaConducirQueryRepository { get; }

    // Módulo Geografía
    ISucursalRepository SucursalRepository { get; }
    SucursalQueryRepository SucursalQueryRepository { get; }

    IHorarioAtencionRepository HorarioAtencionRepository { get; }
    HorarioAtencionQueryRepository HorarioAtencionQueryRepository { get; }

    // Módulo Flota
    ICategoriaVehiculoRepository CategoriaVehiculoRepository { get; }
    CategoriaVehiculoQueryRepository CategoriaVehiculoQueryRepository { get; }

    IVehiculoRepository VehiculoRepository { get; }
    VehiculoQueryRepository VehiculoQueryRepository { get; }

    IMantenimientoRepository MantenimientoRepository { get; }
    MantenimientoQueryRepository MantenimientoQueryRepository { get; }

    // Módulo Comercial
    ISeguroRepository SeguroRepository { get; }
    SeguroQueryRepository SeguroQueryRepository { get; }

    IExtraAdicionalRepository ExtraAdicionalRepository { get; }
    ExtraAdicionalQueryRepository ExtraAdicionalQueryRepository { get; }

    ITarifaRepository TarifaRepository { get; }
    TarifaQueryRepository TarifaQueryRepository { get; }

    // Módulo Transaccional
    IReservaRepository ReservaRepository { get; }
    ReservaQueryRepository ReservaQueryRepository { get; }

    IReservaDetalleRepository ReservaDetalleRepository { get; }
    ReservaDetalleQueryRepository ReservaDetalleQueryRepository { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}