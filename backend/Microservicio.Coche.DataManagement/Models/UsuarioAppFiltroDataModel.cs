using System;

namespace Microservicios.Coche.DataManagement.Models;

public class UsuarioAppFiltroDataModel
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Guid? ROL_id { get; set; }
}