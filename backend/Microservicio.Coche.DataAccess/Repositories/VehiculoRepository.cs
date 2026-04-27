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
    public class VehiculoRepository : IVehiculoRepository
    {
        private readonly CocheDbContext _context;

        public VehiculoRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<VehiculoEntity?> ObtenerPorIdAsync(Guid vehId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<VehiculoEntity>()
                .AsNoTracking()
                .Include(x => x.Categoria)
                .Include(x => x.Sucursal)
                .FirstOrDefaultAsync(x => x.VEH_id == vehId, cancellationToken);
        }

        public async Task<VehiculoEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<VehiculoEntity>()
                .FirstOrDefaultAsync(x => x.VEH_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<VehiculoEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<VehiculoEntity>()
                .AsNoTracking()
                .Include(x => x.Categoria)
                .Include(x => x.Sucursal)
                .OrderBy(x => x.VEH_placa)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(VehiculoEntity vehiculo, CancellationToken cancellationToken = default)
        {
            await _context.Set<VehiculoEntity>().AddAsync(vehiculo, cancellationToken);
        }

        public void Actualizar(VehiculoEntity vehiculo)
        {
            _context.Set<VehiculoEntity>().Update(vehiculo);
        }

        public void Eliminar(VehiculoEntity vehiculo)
        {
            _context.Set<VehiculoEntity>().Remove(vehiculo);
        }
    }
}
