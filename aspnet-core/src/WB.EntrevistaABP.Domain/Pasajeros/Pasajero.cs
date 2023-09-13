using System;
using System.Diagnostics.CodeAnalysis;
using Volo.Abp.Identity;
using Volo.Abp.Domain.Entities.Auditing;
using System.Net;
using System.Collections.Generic;
using WB.EntrevistaABP.Viajes;
using WB.EntrevistaABP.Reservas;
using System.Collections.ObjectModel;
namespace WB.EntrevistaABP.Pasajeros;

public class Pasajero : FullAuditedAggregateRoot<Guid>
{
    public string Nombre { get; set; }
    public string Apellido { get; set; }
    public string DNI { get; set; }
    public DateTime Fecha_de_nacimiento { get; set; }
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public ICollection<Reserva> Reservas { get; set; }
    public Pasajero()
        {
            Reservas = new Collection<Reserva>();
        }

    internal Pasajero(
        Guid id,
        [NotNull] string nombre,
        [NotNull] string apellido,
        [NotNull] string dni,
        [NotNull] string email,
        [NotNull] DateTime fecha_de_nacimiento,
        [NotNull] Guid  usuarioId):base(id)
        {

        Nombre = nombre;
        Apellido = apellido;
        DNI = dni;
        Email = email;
        Fecha_de_nacimiento=fecha_de_nacimiento;
        UserId = usuarioId;
    }
}