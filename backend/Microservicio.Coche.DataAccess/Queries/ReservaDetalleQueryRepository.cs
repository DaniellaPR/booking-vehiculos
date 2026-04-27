using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class ReservaDetalleQueryRepository
    {
        private readonly CocheDbContext _context;

        public ReservaDetalleQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ReservaDetalleEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.ReservaDetalles
                .AsNoTracking()
                .Include(x => x.Seguro) // Traemos la info del seguro si aplica
                .Include(x => x.Extra)  // Traemos la info del extra si aplica
                .OrderBy(x => x.RES_id);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReservaDetalleEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}