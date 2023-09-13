using System;

namespace WB.EntrevistaABP.Reservas;

public class UpdateReservaDto
{
    public Boolean? Coordinador {get; set;}
    public Guid? PasajeroId {get; set;}
    public Guid? ViajeId {get; set;}
}