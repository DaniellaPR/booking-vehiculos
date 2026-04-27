using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class MantenimientoQueryRepository
    {
        private readonly CocheDbContext _context;

        public MantenimientoQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<MantenimientoEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.Mantenimientos
                .AsNoTracking()
                .Include(x => x.Vehiculo) // Vital traer los datos del vehículo
                .OrderByDescending(x => x.MAN_fecha); // Ordenamos del más reciente al más antiguo

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<MantenimientoEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
    }
}