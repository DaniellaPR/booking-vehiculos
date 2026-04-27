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
    public class ReservaRepository : IReservaRepository
    {
        private readonly CocheDbContext _context;

        public ReservaRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<ReservaEntity?> ObtenerPorIdAsync(Guid resId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaEntity>()
                .AsNoTracking()
                .Include(x => x.Cliente)
                .Include(x => x.SucursalRetiro)
                .Include(x => x.SucursalEntrega)
                .Include(x => x.Detalles)
                .FirstOrDefaultAsync(x => x.RES_id == resId, cancellationToken);
        }

        public async Task<IReadOnlyList<ReservaEntity>> ObtenerPorClienteAsync(Guid clienteId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaEntity>()
                .AsNoTracking()
                .Include(x => x.SucursalRetiro)
                .Include(x => x.SucursalEntrega)
                .Where(x => x.CLI_id == clienteId)
                .OrderByDescending(x => x.RES_fechaRetiro)
                .ToListAsync(cancellationToken);
        }

        public async Task<ReservaEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaEntity>()
                .Include(x => x.Detalles)
                .FirstOrDefaultAsync(x => x.RES_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<ReservaEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaEntity>()
                .AsNoTracking()
                .Include(x => x.Cliente)
                .Include(x => x.SucursalRetiro)
                .Include(x => x.SucursalEntrega)
                .OrderByDescending(x => x.RES_fechaRetiro)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(ReservaEntity reserva, CancellationToken cancellationToken = default)
        {
            await _context.Set<ReservaEntity>().AddAsync(reserva, cancellationToken);
        }

        public void Actualizar(ReservaEntity reserva)
        {
            _context.Set<ReservaEntity>().Update(reserva);
        }

        public void Eliminar(ReservaEntity reserva)
        {
            _context.Set<ReservaEntity>().Remove(reserva);
        }
    }
}
