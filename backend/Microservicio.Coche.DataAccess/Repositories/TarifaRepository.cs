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
    public class TarifaRepository : ITarifaRepository
    {
        private readonly CocheDbContext _context;

        public TarifaRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<TarifaEntity?> ObtenerPorIdAsync(Guid tarId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TarifaEntity>()
                .AsNoTracking()
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.TAR_id == tarId, cancellationToken);
        }

        public async Task<TarifaEntity?> ObtenerPorCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TarifaEntity>()
                .AsNoTracking()
                .Include(x => x.Categoria)
                .FirstOrDefaultAsync(x => x.CAT_id == categoriaId, cancellationToken);
        }

        public async Task<TarifaEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TarifaEntity>()
                .FirstOrDefaultAsync(x => x.TAR_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<TarifaEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<TarifaEntity>()
                .AsNoTracking()
                .Include(x => x.Categoria)
                .OrderBy(x => x.TAR_precioDiario)
                .ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<TarifaEntity>> ListarPorCategoriaAsync(Guid categoriaId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<TarifaEntity>()
                .AsNoTracking()
                .Include(x => x.Categoria)
                .Where(x => x.CAT_id == categoriaId)
                .OrderBy(x => x.TAR_precioDiario)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(TarifaEntity tarifa, CancellationToken cancellationToken = default)
        {
            await _context.Set<TarifaEntity>().AddAsync(tarifa, cancellationToken);
        }

        public void Actualizar(TarifaEntity tarifa)
        {
            _context.Set<TarifaEntity>().Update(tarifa);
        }

        public void Eliminar(TarifaEntity tarifa)
        {
            _context.Set<TarifaEntity>().Remove(tarifa);
        }
    }
}
