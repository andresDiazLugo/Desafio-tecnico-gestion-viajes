using System;
using System.ComponentModel.DataAnnotations;

namespace WB.EntrevistaABP.Viajes;

public class CreateViajeDto{
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
    [Required]
    public Guid PasajeroID {get; set;}
}