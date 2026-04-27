using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class CategoriaVehiculoQueryRepository
    {
        private readonly CocheDbContext _context;

        public CategoriaVehiculoQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<CategoriaVehiculoEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.CategoriasVehiculos
                .AsNoTracking()
                .OrderBy(x => x.CAT_nombre);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<CategoriaVehiculoEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}