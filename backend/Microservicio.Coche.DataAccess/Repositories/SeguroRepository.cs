using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories
{
    public class SeguroRepository : ISeguroRepository
    {
        private readonly CocheDbContext _context;

        public SeguroRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<SeguroEntity?> ObtenerPorIdAsync(Guid segId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<SeguroEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.SEG_id == segId, cancellationToken);
        }

        // ✅ FIX: Implementado (antes lanzaba NotImplementedException)
        public async Task<SeguroEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<SeguroEntity>()
                .FirstOrDefaultAsync(x => x.SEG_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<SeguroEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<SeguroEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(SeguroEntity seguro, CancellationToken cancellationToken = default)
        {
            await _context.Set<SeguroEntity>().AddAsync(seguro, cancellationToken);
        }

        public void Actualizar(SeguroEntity seguro)
        {
            _context.Set<SeguroEntity>().Update(seguro);
        }

        public void Eliminar(SeguroEntity seguro)
        {
            _context.Set<SeguroEntity>().Remove(seguro);
        }
    }
}