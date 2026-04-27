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
    public class SucursalRepository : ISucursalRepository
    {
        private readonly CocheDbContext _context;

        public SucursalRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<SucursalEntity?> ObtenerPorIdAsync(Guid sucId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<SucursalEntity>()
                .AsNoTracking()
                .Include(x => x.HorariosAtencion)
                .FirstOrDefaultAsync(x => x.SUC_id == sucId, cancellationToken);
        }

        public async Task<IReadOnlyList<SucursalEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<SucursalEntity>()
                .AsNoTracking()
                .OrderBy(x => x.SUC_nombre)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(SucursalEntity sucursal, CancellationToken cancellationToken = default)
        {
            await _context.Set<SucursalEntity>().AddAsync(sucursal, cancellationToken);
        }

        public void Actualizar(SucursalEntity sucursal)
        {
            _context.Set<SucursalEntity>().Update(sucursal);
        }

        public void Eliminar(SucursalEntity sucursal)
        {
            _context.Set<SucursalEntity>().Remove(sucursal);
        }

        public async Task<SucursalEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<SucursalEntity>()
                .FirstOrDefaultAsync(x => x.SUC_id == id, cancellationToken);
        }
    }
}
