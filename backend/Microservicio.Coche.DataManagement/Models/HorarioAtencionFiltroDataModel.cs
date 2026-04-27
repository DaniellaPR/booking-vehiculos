using System;

namespace Microservicios.Coche.DataManagement.Models;

public class HorarioAtencionFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? SUC_id { get; set; }
}