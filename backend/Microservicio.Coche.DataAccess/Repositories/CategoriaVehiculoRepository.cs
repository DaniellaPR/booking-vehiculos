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
    public class CategoriaVehiculoRepository : ICategoriaVehiculoRepository
    {
        private readonly CocheDbContext _context;

        public CategoriaVehiculoRepository(CocheDbContext context)
        {
            _context = context;
        }

        public async Task<CategoriaVehiculoEntity?> ObtenerPorIdAsync(Guid catId, CancellationToken cancellationToken = default)
        {
            return await _context.Set<CategoriaVehiculoEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.CAT_id == catId, cancellationToken);
        }

        // ✅ FIX: Implementado (antes lanzaba NotImplementedException)
        public async Task<CategoriaVehiculoEntity> ObtenerParaActualizarAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Set<CategoriaVehiculoEntity>()
                .FirstOrDefaultAsync(x => x.CAT_id == id, cancellationToken)
                ?? throw new KeyNotFoundException($"No se encontró CategoriaVehiculo con id {id}");
        }

        public async Task<IReadOnlyList<CategoriaVehiculoEntity>> ListarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Set<CategoriaVehiculoEntity>()
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task AgregarAsync(CategoriaVehiculoEntity categoria, CancellationToken cancellationToken = default)
        {
            await _context.Set<CategoriaVehiculoEntity>().AddAsync(categoria, cancellationToken);
        }

        public void Actualizar(CategoriaVehiculoEntity categoria)
        {
            _context.Set<CategoriaVehiculoEntity>().Update(categoria);
        }

        public void Eliminar(CategoriaVehiculoEntity categoria)
        {
            _context.Set<CategoriaVehiculoEntity>().Remove(categoria);
        }
    }
}