using System;

namespace Microservicios.Coche.DataManagement.Models;

public class MantenimientoFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? VEH_id { get; set; }
}