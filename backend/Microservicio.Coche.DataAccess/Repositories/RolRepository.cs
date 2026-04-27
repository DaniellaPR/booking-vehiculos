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
    public class RolRepository : IRolRepository
    {
        private readonly CocheDbContext _context;

        public RolRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<RolEntity?> ObtenerPorIdAsync(Guid rolId, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.ROL_id == rolId, cancellationToken);
        }

        public async Task<RolEntity?> ObtenerParaActualizarAsync(Guid rolId, CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(x => x.ROL_id == rolId, cancellationToken);
        }

        public async Task<IReadOnlyList<RolEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .AsNoTracking()
                .OrderBy(x => x.ROL_nombre)
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(RolEntity rol, CancellationToken cancellationToken = default)
        {
            await _context.Roles.AddAsync(rol, cancellationToken);
        }

        public void Actualizar(RolEntity rol)
        {
            _context.Roles.Update(rol);
        }

        public void Eliminar(RolEntity rol)
        {
            _context.Roles.Remove(rol);
        }
    }
}
