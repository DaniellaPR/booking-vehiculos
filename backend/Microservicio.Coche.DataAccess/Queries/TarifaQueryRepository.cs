using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class TarifaQueryRepository
    {
        private readonly CocheDbContext _context;

        public TarifaQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TarifaEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.Tarifas
                .AsNoTracking()
                .Include(x => x.Categoria) // Vital incluir la categoría para saber a qué auto aplica
                .OrderBy(x => x.TAR_precioDiario);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<TarifaEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}