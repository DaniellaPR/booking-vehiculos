using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class ClienteQueryRepository
    {
        private readonly CocheDbContext _context;

        public ClienteQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ClienteEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            // Consulta base de solo lectura
            var query = _context.Clientes
                .AsNoTracking()
                .OrderBy(x => x.CLI_apellidos) // Ordenamos por apellido como buena práctica
                .ThenBy(x => x.CLI_nombres);

            // Contamos el total de registros (usando long como tu profesor)
            var totalRecords = await query.LongCountAsync(cancellationToken);

            // Aplicamos la paginación
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            // Retornamos el DTO de paginación
            return new PagedResult<ClienteEntity>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRecords = totalRecords
            };
        }

        public async Task<IEnumerable<ClienteEntity>> ListarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}