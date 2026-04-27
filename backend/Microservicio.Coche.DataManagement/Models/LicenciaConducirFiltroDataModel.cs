using System;

namespace Microservicios.Coche.DataManagement.Models;

public class LicenciaConducirFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? CLI_id { get; set; }
}