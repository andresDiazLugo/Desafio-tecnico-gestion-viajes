using System;
using Volo.Abp.Application.Dtos;
using WB.EntrevistaABP.Pasajeros;
using WB.EntrevistaABP.Viajes;

namespace WB.EntrevistaABP.Reservas;

public class ReservaDto : EntityDto<Guid>
{
    public Boolean? Coordinador {get; set;}
    public Guid? PasajeroId {get; set;}
    public PasajeroDto Pasajero { get; set; }
    public Guid? VIajeId {get; set;}
    public ViajeDto Viaje { get; set; }

}