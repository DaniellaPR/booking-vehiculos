using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class ExtraAdicionalQueryRepository
    {
        private readonly CocheDbContext _context;

        public ExtraAdicionalQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ExtraAdicionalEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.ExtrasAdicionales
                .AsNoTracking()
                .OrderBy(x => x.EXT_nombre);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<ExtraAdicionalEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}