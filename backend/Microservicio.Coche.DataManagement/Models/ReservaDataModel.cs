using System;

namespace Microservicios.Coche.DataManagement.Models;

public class ReservaDataModel
{
    public Guid RES_id { get; set; }
    public Guid CLI_id { get; set; }
    public Guid RES_sucursalRetiroId { get; set; }
    public Guid RES_sucursalEntregaId { get; set; }
    public DateTime RES_fechaRetiro { get; set; }
    public DateTime RES_fechaEntrega { get; set; }
    public string? RES_estado { get; set; }
    public DateTime? RES_fechaCreacion { get; set; }
    public string? RES_usuarioCreacion { get; set; }
    public DateTime? RES_fechaModificacion { get; set; }
    public string? RES_usuarioModificacion { get; set; }
}