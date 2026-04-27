using Microservicios.Coche.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Microservicios.Coche.DataAccess.Common
{
    public abstract class RepositoryBase<TEntity> where TEntity : class
    {
        protected readonly CocheDbContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        protected RepositoryBase(CocheDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task AgregarAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public virtual void Actualizar(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Eliminar(TEntity entity)
        {
            _dbSet.Remove(entity);
        }
    }
}