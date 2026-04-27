using System;

namespace Microservicios.Coche.DataManagement.Models;

public class VehiculoFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? VEH_placa { get; set; }
    public Guid? CAT_id { get; set; }
    public Guid? SUC_id { get; set; }
}