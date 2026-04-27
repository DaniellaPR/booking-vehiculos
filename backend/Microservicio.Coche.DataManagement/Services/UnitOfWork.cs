using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Queries;
using Microservicios.Coche.DataAccess.Repositories;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;
using Microservicios.Coche.DataManagement.Interfaces;

namespace Microservicios.Coche.DataManagement.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly CocheDbContext _context;

    public IAuditoriaRepository AuditoriaRepository { get; }
    public AuditoriaQueryRepository AuditoriaQueryRepository { get; }
    public IRolRepository RolRepository { get; }
    public RolQueryRepository RolQueryRepository { get; }
    public IUsuarioAppRepository UsuarioAppRepository { get; }
    public UsuarioAppQueryRepository UsuarioAppQueryRepository { get; }

    public IClienteRepository ClienteRepository { get; }
    public ClienteQueryRepository ClienteQueryRepository { get; }
    public ILicenciaConducirRepository LicenciaConducirRepository { get; }
    public LicenciaConducirQueryRepository LicenciaConducirQueryRepository { get; }

    public ISucursalRepository SucursalRepository { get; }
    public SucursalQueryRepository SucursalQueryRepository { get; }
    public IHorarioAtencionRepository HorarioAtencionRepository { get; }
    public HorarioAtencionQueryRepository HorarioAtencionQueryRepository { get; }

    public ICategoriaVehiculoRepository CategoriaVehiculoRepository { get; }
    public CategoriaVehiculoQueryRepository CategoriaVehiculoQueryRepository { get; }
    public IVehiculoRepository VehiculoRepository { get; }
    public VehiculoQueryRepository VehiculoQueryRepository { get; }
    public IMantenimientoRepository MantenimientoRepository { get; }
    public MantenimientoQueryRepository MantenimientoQueryRepository { get; }

    public ISeguroRepository SeguroRepository { get; }
    public SeguroQueryRepository SeguroQueryRepository { get; }
    public IExtraAdicionalRepository ExtraAdicionalRepository { get; }
    public ExtraAdicionalQueryRepository ExtraAdicionalQueryRepository { get; }
    public ITarifaRepository TarifaRepository { get; }
    public TarifaQueryRepository TarifaQueryRepository { get; }

    public IReservaRepository ReservaRepository { get; }
    public ReservaQueryRepository ReservaQueryRepository { get; }
    public IReservaDetalleRepository ReservaDetalleRepository { get; }
    public ReservaDetalleQueryRepository ReservaDetalleQueryRepository { get; }

    public UnitOfWork(CocheDbContext context)
    {
        _context = context;

        AuditoriaRepository = new AuditoriaRepository(_context);
        AuditoriaQueryRepository = new AuditoriaQueryRepository(_context);
        RolRepository = new RolRepository(_context);
        RolQueryRepository = new RolQueryRepository(_context);
        UsuarioAppRepository = new UsuarioAppRepository(_context);
        UsuarioAppQueryRepository = new UsuarioAppQueryRepository(_context);

        ClienteRepository = new ClienteRepository(_context);
        ClienteQueryRepository = new ClienteQueryRepository(_context);
        LicenciaConducirRepository = new LicenciaConducirRepository(_context);
        LicenciaConducirQueryRepository = new LicenciaConducirQueryRepository(_context);

        SucursalRepository = new SucursalRepository(_context);
        SucursalQueryRepository = new SucursalQueryRepository(_context);
        HorarioAtencionRepository = new HorarioAtencionRepository(_context);
        HorarioAtencionQueryRepository = new HorarioAtencionQueryRepository(_context);

        CategoriaVehiculoRepository = new CategoriaVehiculoRepository(_context);
        CategoriaVehiculoQueryRepository = new CategoriaVehiculoQueryRepository(_context);
        VehiculoRepository = new VehiculoRepository(_context);
        VehiculoQueryRepository = new VehiculoQueryRepository(_context);
        MantenimientoRepository = new MantenimientoRepository(_context);
        MantenimientoQueryRepository = new MantenimientoQueryRepository(_context);

        SeguroRepository = new SeguroRepository(_context);
        SeguroQueryRepository = new SeguroQueryRepository(_context);
        ExtraAdicionalRepository = new ExtraAdicionalRepository(_context);
        ExtraAdicionalQueryRepository = new ExtraAdicionalQueryRepository(_context);
        TarifaRepository = new TarifaRepository(_context);
        TarifaQueryRepository = new TarifaQueryRepository(_context);

        ReservaRepository = new ReservaRepository(_context);
        ReservaQueryRepository = new ReservaQueryRepository(_context);
        ReservaDetalleRepository = new ReservaDetalleRepository(_context);
        ReservaDetalleQueryRepository = new ReservaDetalleQueryRepository(_context);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}