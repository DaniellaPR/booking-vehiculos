using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories
{
    public class MantenimientoRepository : IMantenimientoRepository
    {
        private readonly CocheDbContext _context;

        public MantenimientoRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<MantenimientoEntity?> ObtenerPorIdAsync(Guid manId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<MantenimientoEntity>()
                .AsNoTracking()
                .Include(x => x.Vehiculo)
                .FirstOrDefaultAsync(x => x.MAN_id == manId, cancellationToken);
        }

        public async Task<IReadOnlyList<MantenimientoEntity>> ObtenerPorVehiculoAsync(Guid vehiculoId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<MantenimientoEntity>()
                .AsNoTracking()
                .Include(x => x.Vehiculo)
                .Where(x => x.VEH_id == vehiculoId)
                .OrderByDescending(x => x.MAN_fecha)
                .ToListAsync(cancellationToken);
        }

        public async Task<MantenimientoEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<MantenimientoEntity>()
                .FirstOrDefaultAsync(x => x.MAN_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<MantenimientoEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<MantenimientoEntity>()
                .AsNoTracking()
                .Include(x => x.Vehiculo)
                .OrderByDescending(x => x.MAN_fecha)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(MantenimientoEntity mantenimiento, CancellationToken cancellationToken = default)
        {
            await _context.Set<MantenimientoEntity>().AddAsync(mantenimiento, cancellationToken);
        }

        public void Actualizar(MantenimientoEntity mantenimiento)
        {
            _context.Set<MantenimientoEntity>().Update(mantenimiento);
        }

        public void Eliminar(MantenimientoEntity mantenimiento)
        {
            _context.Set<MantenimientoEntity>().Remove(mantenimiento);
        }
    }
}
