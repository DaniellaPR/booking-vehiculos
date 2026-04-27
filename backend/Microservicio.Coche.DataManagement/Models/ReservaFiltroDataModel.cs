using System;

namespace Microservicios.Coche.DataManagement.Models;

public class ReservaFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CLI_id { get; set; }
    public string? RES_estado { get; set; }
}