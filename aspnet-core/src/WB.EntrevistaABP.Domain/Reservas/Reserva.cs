using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using WB.EntrevistaABP.Pasajeros;
using WB.EntrevistaABP.Viajes;



namespace WB.EntrevistaABP.Reservas;

public class Reserva : FullAuditedAggregateRoot<Guid>
{
    public Boolean Coordinador { get; set; }
    public Guid PasajeroId { get; set; }
    public Pasajero Pasajero { get; set; }
    public Guid ViajeId { get; set; }
    public Viaje Viaje { get; set; }


    private Reserva()
    {

    } 
    internal Reserva(
        Guid id,
        [NotNull] Boolean coordiandor,
        [NotNull] Guid pasajeroId,
        [NotNull] Guid viajeId
    ): base(id)
    {
    Coordinador = coordiandor;
    PasajeroId = pasajeroId;
    ViajeId = viajeId;
    }
 
}