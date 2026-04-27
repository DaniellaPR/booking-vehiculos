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
    public class ExtraAdicionalRepository : IExtraAdicionalRepository
    {
        private readonly CocheDbContext _context;

        public ExtraAdicionalRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<ExtraAdicionalEntity?> ObtenerPorIdAsync(Guid extId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ExtraAdicionalEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.EXT_id == extId, cancellationToken);
        }

        // ✅ FIX: Implementado (antes lanzaba NotImplementedException)
        public async Task<ExtraAdicionalEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ExtraAdicionalEntity>()
                .FirstOrDefaultAsync(x => x.EXT_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<ExtraAdicionalEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<ExtraAdicionalEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(ExtraAdicionalEntity extra, CancellationToken cancellationToken = default)
        {
            await _context.Set<ExtraAdicionalEntity>().AddAsync(extra, cancellationToken);
        }

        public void Actualizar(ExtraAdicionalEntity extra)
        {
            _context.Set<ExtraAdicionalEntity>().Update(extra);
        }

        public void Eliminar(ExtraAdicionalEntity extra)
        {
            _context.Set<ExtraAdicionalEntity>().Remove(extra);
        }
    }
}