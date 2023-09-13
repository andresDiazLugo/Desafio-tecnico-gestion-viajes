using System;
using Volo.Abp.Application.Dtos;


namespace WB.EntrevistaABP.Reservas;

public class PasajeroLookupDto: EntityDto<Guid>
{
  
    public string Nombre { get; set; }

    public string DNI { get; set; }


}