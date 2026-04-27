using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class HorarioAtencionQueryRepository
    {
        private readonly CocheDbContext _context;

        public HorarioAtencionQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<HorarioAtencionEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.HorariosAtencion
                .AsNoTracking()
                .Include(x => x.Sucursal) // Incluimos la sucursal para que la consulta tenga contexto
                .OrderBy(x => x.SUC_id);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<HorarioAtencionEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}