using System;

namespace Microservicios.Coche.DataManagement.Models;

public class TarifaFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CAT_id { get; set; }
}