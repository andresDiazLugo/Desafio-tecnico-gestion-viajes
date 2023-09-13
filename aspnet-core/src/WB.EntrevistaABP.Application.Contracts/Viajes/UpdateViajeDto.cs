using System;
using System.ComponentModel.DataAnnotations;

namespace WB.EntrevistaABP.Viajes;

public class UpdateViajeDto
{
    [Required]
    public DateTime Fecha_de_salida { get; set; }
    [Required]
    public DateTime Fecha_de_llegada { get; set; }
    [Required]
    public string Origen {get; set;}
    [Required]
    public string Destino {get; set;}
    [Required]
    public ViajeType Medio_transporte {get; set;}
}