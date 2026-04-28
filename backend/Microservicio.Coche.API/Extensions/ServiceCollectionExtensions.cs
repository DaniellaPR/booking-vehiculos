using Microservicios.coche.Business.Interfaces;
using Microservicios.Coche.Business.Interfaces;
using Microservicios.Coche.Business.Services;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataManagement.Interfaces;
using Microservicios.Coche.DataManagement.Services;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Microservicios.Coche.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Conexión a Supabase (PostgreSQL)
        services.AddDbContext<CocheDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CocheDb")));

        // Registro del UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // ==========================================
        // CAPA: DataManagement (Servicios de Datos)
        // ==========================================
        services.AddScoped<IClienteDataService, ClienteDataService>();
        services.AddScoped<IVehiculoDataService, VehiculoDataService>();
        services.AddScoped<IReservaDataService, ReservaDataService>();
        services.AddScoped<IRolDataService, RolDataService>();
        services.AddScoped<ICategoriaVehiculoDataService, CategoriaVehiculoDataService>();
        services.AddScoped<ISucursalDataService, SucursalDataService>();
        services.AddScoped<IAuditoriaDataService, AuditoriaDataService>();

        services.AddScoped<IExtraAdicionalDataService, ExtraAdicionalDataService>();
        services.AddScoped<ISeguroDataService, SeguroDataService>();
        services.AddScoped<ITarifaDataService, TarifaDataService>();
        services.AddScoped<IUsuarioAppDataService, UsuarioAppDataService>();

        // ==========================================
        // CAPA: Business (Servicios de Negocio)
        // ==========================================
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IVehiculoService, VehiculoService>();
        services.AddScoped<IReservaService, ReservaService>();
        services.AddScoped<IRolService, RolService>();
        services.AddScoped<ICategoriaVehiculoService, CategoriaVehiculoService>();
        services.AddScoped<IExtraAdicionalService, ExtraAdicionalService>();
        services.AddScoped<ISeguroService, SeguroService>();
        services.AddScoped<ITarifaService, TarifaService>();
        services.AddScoped<IUsuarioAppService, UsuarioAppService>();
        services.AddScoped<ISucursalService, SucursalService>();
        services.AddScoped<IAuditoriaService, AuditoriaService>(); // Indispensable para el Login y JWT
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
