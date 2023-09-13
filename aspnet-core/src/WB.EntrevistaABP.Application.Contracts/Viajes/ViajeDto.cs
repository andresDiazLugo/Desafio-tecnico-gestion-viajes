using System;
using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Viajes;

public class ViajeDto : EntityDto<Guid>
{
    public DateTime Fecha_de_salida { get; set; }
    public DateTime Fecha_de_llegada { get; set; }
    public string Origen {get; set;}
    public string Destino {get; set;}
    public ViajeType Medio_transporte {get; set;}

}