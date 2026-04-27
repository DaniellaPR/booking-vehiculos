using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories
{
    public class UsuarioAppRepository : IUsuarioAppRepository
    {
        private readonly CocheDbContext _context;

        public UsuarioAppRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioAppEntity?> ObtenerPorIdAsync(Guid usuId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<UsuarioAppEntity>()
                .AsNoTracking()
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.USU_id == usuId, cancellationToken);
        }

        public async Task<UsuarioAppEntity?> ObtenerPorEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await _context.Set<UsuarioAppEntity>()
                .AsNoTracking()
                .Include(x => x.Rol)
                .FirstOrDefaultAsync(x => x.USU_email == email, cancellationToken);
        }

        public async Task<UsuarioAppEntity?> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<UsuarioAppEntity>()
                .FirstOrDefaultAsync(x => x.USU_id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<UsuarioAppEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<UsuarioAppEntity>()
                .AsNoTracking()
                .Include(x => x.Rol)
                .OrderBy(x => x.USU_email)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(UsuarioAppEntity usuario, CancellationToken cancellationToken = default)
        {
            await _context.Set<UsuarioAppEntity>().AddAsync(usuario, cancellationToken);
        }

        public void Actualizar(UsuarioAppEntity usuario)
        {
            _context.Set<UsuarioAppEntity>().Update(usuario);
        }

        public void Eliminar(UsuarioAppEntity usuario)
        {
            _context.Set<UsuarioAppEntity>().Remove(usuario);
        }
    }
}
