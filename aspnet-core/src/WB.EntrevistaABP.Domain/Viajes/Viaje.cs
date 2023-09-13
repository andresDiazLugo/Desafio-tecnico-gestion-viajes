using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using WB.EntrevistaABP.Reservas;



namespace WB.EntrevistaABP.Viajes;

public class Viaje : FullAuditedAggregateRoot<Guid>
{
    public DateTime Fecha_de_salida { get; set; }
    public DateTime Fecha_de_llegada { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public ViajeType Medio_transporte { get; set; }
    public List<Guid> ListaUsuarios { get; set; }
    public ICollection<Reserva> Reservas { get; set; }
    private Viaje()
    {

        Reservas = new Collection<Reserva>();
        
    } 
    internal Viaje(
        Guid id,
        [NotNull] DateTime fecha_de_salida,
        [NotNull] DateTime fecha_de_llegada,
        [NotNull] string origen,
        [NotNull] string destino,
        [NotNull] ViajeType medio_transporte,
        [NotNull] List<Guid> listaUsuarios
    ): base(id)
    {
    Origen = origen;
    Destino = destino;
    Fecha_de_salida = fecha_de_salida;
    Fecha_de_llegada = fecha_de_llegada;
    Medio_transporte = medio_transporte;
    ListaUsuarios= listaUsuarios;
    }   
}