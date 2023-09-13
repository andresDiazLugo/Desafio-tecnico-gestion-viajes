using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Reservas;

public class CreateReservaDto : EntityDto<Guid>
{
    [Required]
    public Boolean? Coordinador { get; set; }
    [Required]
    public Guid? PasajeroId { get; set; }
    [Required]
    public Guid? VIajeId { get; set; }

}