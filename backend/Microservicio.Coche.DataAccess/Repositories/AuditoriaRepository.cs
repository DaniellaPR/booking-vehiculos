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
    public class AuditoriaRepository : IAuditoriaRepository
    {
        private readonly CocheDbContext _context;

        public AuditoriaRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<AuditoriaEntity?> ObtenerPorIdAsync(Guid audId, CancellationToken cancellationToken = default)
        {
            return await _context.Auditorias
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.AUD_id == audId, cancellationToken);
        }

        public async Task<IReadOnlyList<AuditoriaEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Auditorias
                .AsNoTracking()
                .OrderByDescending(x => x.AUD_fecha)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(AuditoriaEntity auditoria, CancellationToken cancellationToken = default)
        {
            await _context.Auditorias.AddAsync(auditoria, cancellationToken);
        }
    }
}
