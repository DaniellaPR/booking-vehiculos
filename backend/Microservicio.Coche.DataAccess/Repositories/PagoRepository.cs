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
    public class PagoRepository : IPagoRepository
    {
        private readonly CocheDbContext _context;

        public PagoRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagoEntity?> ObtenerPorIdAsync(Guid pagId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<PagoEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.PAG_id == pagId, cancellationToken);
        }

        public async Task<IReadOnlyList<PagoEntity>> ObtenerPorReservaAsync(Guid resId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<PagoEntity>()
                .AsNoTracking()
                .Where(x => x.RES_id == resId)
                .OrderByDescending(x => x.PAG_fechaPago)
                .ToListAsync(cancellationToken);
        }

        public async Task<PagoEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<PagoEntity>()
                .FirstOrDefaultAsync(x => x.PAG_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<PagoEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<PagoEntity>()
                .AsNoTracking()
                .OrderByDescending(x => x.PAG_fechaPago)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(PagoEntity pago, CancellationToken cancellationToken = default)
        {
            await _context.Set<PagoEntity>().AddAsync(pago, cancellationToken);
        }

        public void Actualizar(PagoEntity pago)
        {
            _context.Set<PagoEntity>().Update(pago);
        }

        public void Eliminar(PagoEntity pago)
        {
            _context.Set<PagoEntity>().Remove(pago);
        }
    }
}