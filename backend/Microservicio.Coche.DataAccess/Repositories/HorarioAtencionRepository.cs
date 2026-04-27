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
    public class HorarioAtencionRepository : IHorarioAtencionRepository
    {
        private readonly CocheDbContext _context;

        public HorarioAtencionRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<HorarioAtencionEntity?> ObtenerPorIdAsync(Guid horId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<HorarioAtencionEntity>()
                .AsNoTracking()
                .Include(x => x.Sucursal)
                .FirstOrDefaultAsync(x => x.HOR_id == horId, cancellationToken);
        }

        public async Task<HorarioAtencionEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<HorarioAtencionEntity>()
                .FirstOrDefaultAsync(x => x.HOR_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<HorarioAtencionEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<HorarioAtencionEntity>()
                .AsNoTracking()
                .Include(x => x.Sucursal)
                .OrderBy(x => x.HOR_diaSemana)
                .ThenBy(x => x.HOR_apertura)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<HorarioAtencionEntity>> ObtenerPorSucursalAsync(Guid sucursalId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<HorarioAtencionEntity>()
                .AsNoTracking()
                .Where(x => x.SUC_id == sucursalId)
                .OrderBy(x => x.HOR_diaSemana)
                .ThenBy(x => x.HOR_apertura)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(HorarioAtencionEntity horario, CancellationToken cancellationToken = default)
        {
            await _context.Set<HorarioAtencionEntity>().AddAsync(horario, cancellationToken);
        }

        public void Actualizar(HorarioAtencionEntity horario)
        {
            _context.Set<HorarioAtencionEntity>().Update(horario);
        }

        public void Eliminar(HorarioAtencionEntity horario)
        {
            _context.Set<HorarioAtencionEntity>().Remove(horario);
        }
    }
}
