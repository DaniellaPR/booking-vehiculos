using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microservicios.Coche.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Repositories
{
    public class LicenciaConducirRepository : ILicenciaConducirRepository
    {
        private readonly CocheDbContext _context;

        public LicenciaConducirRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<LicenciaConducirEntity?> ObtenerPorIdAsync(Guid licId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<LicenciaConducirEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.LIC_id == licId, cancellationToken);
        }

        public async Task AgregarAsync(LicenciaConducirEntity licencia, CancellationToken cancellationToken = default)
        {
            await _context.Set<LicenciaConducirEntity>().AddAsync(licencia, cancellationToken);
        }

        public void Actualizar(LicenciaConducirEntity licencia)
        {
            _context.Set<LicenciaConducirEntity>().Update(licencia);
        }
        public void Eliminar(LicenciaConducirEntity licencia)
        {
            _context.Set<LicenciaConducirEntity>().Remove(licencia);
        }

        public Task<LicenciaConducirEntity> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<object>> ListarAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}