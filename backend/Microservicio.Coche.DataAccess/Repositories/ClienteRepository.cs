using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly CocheDbContext _context;

        public ClienteRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<ClienteEntity?> ObtenerPorIdAsync(Guid cliId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ClienteEntity>()
                .AsNoTracking()
                .Include(x => x.Licencias)
                .FirstOrDefaultAsync(x => x.CLI_id == cliId, cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerPorCedulaAsync(string cedula, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ClienteEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CLI_cedula == cedula, cancellationToken);
        }

        public async Task<IReadOnlyList<ClienteEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<ClienteEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<ClienteEntity?> ObtenerParaActualizarAsync(Guid cliId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<ClienteEntity>()
                .FirstOrDefaultAsync(x => x.CLI_id == cliId, cancellationToken);
        }

        public async Task AgregarAsync(ClienteEntity cliente, CancellationToken cancellationToken = default)
        {
            await _context.Set<ClienteEntity>().AddAsync(cliente, cancellationToken);
        }

        public void Actualizar(ClienteEntity cliente)
        {
            _context.Set<ClienteEntity>().Update(cliente);
        }

        public void Eliminar(ClienteEntity cliente)
        {
            _context.Set<ClienteEntity>().Remove(cliente);
        }
    }
}
