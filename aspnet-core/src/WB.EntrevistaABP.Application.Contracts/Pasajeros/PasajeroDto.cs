using System;
using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Pasajeros;

public class PasajeroDto : EntityDto<Guid>
{
    
    public string? Nombre {get; set;}
    public string? Apellido {get; set;}
    public string? DNI {get; set;}
    public string? Email {get; set;}
    public string? Password {get; set;}
    public DateTime Fecha_de_nacimiento {get; set;}
    public Guid UserId {get; set;}


}