using System;
using Volo.Abp.Application.Dtos;


namespace WB.EntrevistaABP.Reservas;

public class ViajeLookupDto: EntityDto<Guid>
{
  
    public string Origen { get; set; }
    public string Destino { get; set; }
    public string fecha_de_salida { get; set; }
    public string fecha_de_llegada { get; set; }

}