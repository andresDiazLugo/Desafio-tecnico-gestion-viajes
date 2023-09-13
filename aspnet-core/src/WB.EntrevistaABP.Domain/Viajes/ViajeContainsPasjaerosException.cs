using Volo.Abp;
using WB.EntrevistaABP;

namespace WB.EntrevistaABP.Viajes;

public class ViajeContainsPasjaerosException : BusinessException
{
    public ViajeContainsPasjaerosException()
        : base(EntrevistaABPDomainErrorCodes.ViajeContainsPasajeros)
    {
        WithData("DeleteViaje", "No se puede eliminar un viaje que contenga pasajeros");
    }
}