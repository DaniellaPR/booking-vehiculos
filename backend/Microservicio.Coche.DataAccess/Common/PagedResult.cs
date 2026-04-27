using System;
using System.Collections.Generic;

namespace Microservicios.Coche.DataAccess.Common
{
    public class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; set; } = new List<T>();
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalRecords { get; set; } // Ajustado a long según el LongCountAsync del profesor

        public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalRecords / (double)PageSize) : 0;
    }
}