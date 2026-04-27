using Microservicios.Coche.DataAccess.Common;
using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Queries
{
    public class ReservaQueryRepository
    {
        private readonly CocheDbContext _context;

        public ReservaQueryRepository(CocheDbContext context)
        {
            _context = context;
        }

        // Corregido: devolver un resultado paginado en lugar de Task (void) para poder asignarlo al var await ...
        public Task<PagedResult<ReservaEntity>> BuscarAsync(int pageNumber, int pageSize, Guid? cLI_id, string? rES_estado, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        // Sobrecarga que devuelve PagedResult también
        public Task<PagedResult<ReservaEntity>> BuscarAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResult<ReservaEntity>> BuscarHistorialAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}