using Microservicios.Coche.DataAccess.Context;
using Microservicios.Coche.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// ¡Mira, mamá, sin usar DataManagement!
namespace Microservicios.Coche.DataAccess.Queries;

public class PagoQueryRepository
{
    private readonly CocheDbContext _context;

    public PagoQueryRepository(CocheDbContext context)
    {
        _context = context;
    }

    // Usamos una Tupla de C# para devolver múltiples valores de forma limpia
    public async Task<(IReadOnlyList<PagoEntity> Items, long TotalRecords, int PageNumber, int PageSize)> BuscarAsync(int pageNumber, int pageSize, Guid? reservaId, string? estado, CancellationToken cancellationToken = default)
    {
        var query = _context.Set<PagoEntity>().AsNoTracking().AsQueryable();

        if (reservaId.HasValue)
            query = query.Where(x => x.RES_id == reservaId.Value);

        if (!string.IsNullOrEmpty(estado))
            query = query.Where(x => x.PAG_estado == estado);

        var totalRecords = await query.LongCountAsync(cancellationToken);

        var items = await query
            .OrderByDescending(x => x.PAG_fechaPago)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        // Retornamos la tupla con los resultados
        return (items, totalRecords, pageNumber, pageSize);
    }
}