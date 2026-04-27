
using Microservicios.Coche.DataManagement.Models;

namespace Microservicios.Coche.Business.DTOs.Sucursal
{
    public class SucursalFiltroRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? NombreContains { get; set; }
        public string? CiudadEquals { get; set; }
    }
}
