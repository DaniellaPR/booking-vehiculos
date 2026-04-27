using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class LicenciaConducirQueryRepository
    {
        private readonly CocheDbContext _context;

        public LicenciaConducirQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<LicenciaConducirEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.LicenciasConducir
                .AsNoTracking()
                .Include(x => x.CLI_id)
                .OrderBy(x => x.LIC_vigencia); // Ordenado por fecha de caducidad por defecto

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<LicenciaConducirEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}