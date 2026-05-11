using System;

namespace Microservicios.Coche.DataManagement.Models;

public class PagoFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? RES_id { get; set; }
    public string? PAG_estado { get; set; }
}