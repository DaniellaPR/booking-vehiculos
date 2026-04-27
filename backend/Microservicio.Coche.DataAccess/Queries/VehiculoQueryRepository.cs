using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class VehiculoQueryRepository
    {
        private readonly CocheDbContext _context;

        public VehiculoQueryRepository(CocheDbContext context)
        {
            _context = context;
        }


        public async Task<PagedResult<VehiculoEntity>> BuscarDisponiblesAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            // Aquí sí aplicamos un Where útil para el negocio: Solo buscar los "Disponibles"
            var query = _context.Vehiculos
                .AsNoTracking()
                .Include(x => x.Categoria) // Incluimos info de la categoría para la UI
                .Where(x => x.VEH_estado == "Disponible")
                .OrderBy(x => x.VEH_id);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<VehiculoEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }
        public Task<PagedResult<VehiculoEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

    }
}