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
    public class ReservaDetalleRepository : IReservaDetalleRepository
    {
        private readonly CocheDbContext _context;

        public ReservaDetalleRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<ReservaDetalleEntity?> ObtenerPorIdAsync(Guid rexId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaDetalleEntity>()
                .AsNoTracking()
                .Include(x => x.Reserva)
                .Include(x => x.Seguro)
                .Include(x => x.Extra)
                .FirstOrDefaultAsync(x => x.REX_id == rexId, cancellationToken);
        }

        public async Task<ReservaDetalleEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaDetalleEntity>()
                .FirstOrDefaultAsync(x => x.REX_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<ReservaDetalleEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<ReservaDetalleEntity>()
                .AsNoTracking()
                .Include(x => x.Reserva)
                .Include(x => x.Seguro)
                .Include(x => x.Extra)
                .OrderBy(x => x.RES_id)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(ReservaDetalleEntity detalle, CancellationToken cancellationToken = default)
        {
            await _context.Set<ReservaDetalleEntity>().AddAsync(detalle, cancellationToken);
        }

        public void Actualizar(ReservaDetalleEntity detalle)
        {
            _context.Set<ReservaDetalleEntity>().Update(detalle);
        }

        public void Eliminar(ReservaDetalleEntity detalle)
        {
            _context.Set<ReservaDetalleEntity>().Remove(detalle);
        }
    }
}
