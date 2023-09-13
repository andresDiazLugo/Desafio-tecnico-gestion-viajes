using Volo.Abp;
using WB.EntrevistaABP;

namespace WB.EntrevistaABP.Pasajeros;

public class PasajeroAlreadyExistsException : BusinessException
{
    public PasajeroAlreadyExistsException(string dni)
        : base(EntrevistaABPDomainErrorCodes.PasajeroAlreadyExists)
    {
        WithData("DNI", dni);
    }
}