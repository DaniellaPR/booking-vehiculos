using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class UsuarioAppQueryRepository
    {
        private readonly CocheDbContext _context;

        public UsuarioAppQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<UsuarioAppEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var query = _context.UsuariosApp
                .AsNoTracking()
                .Include(x => x.Rol)
                .OrderBy(x => x.USU_email);

            var totalRecords = await query.LongCountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PagedResult<UsuarioAppEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<IEnumerable<UsuarioAppEntity>> ListarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}