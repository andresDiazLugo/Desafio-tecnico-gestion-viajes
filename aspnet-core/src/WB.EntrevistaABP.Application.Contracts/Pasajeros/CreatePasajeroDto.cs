using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Pasajeros;

public class CreatePasajeroDto : EntityDto<Guid>
{
    [Required]
    public string Nombre {get; set;}
    [Required]
    public string Apellido {get; set;}
    [Required]
    public string DNI {get; set;}
    [Required]
    public string Email {get; set;}
    [Required]
    public DateTime Fecha_de_nacimiento {get; set;}
}