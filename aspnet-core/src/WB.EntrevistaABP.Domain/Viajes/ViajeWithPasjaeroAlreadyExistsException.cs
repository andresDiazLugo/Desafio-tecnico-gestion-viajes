using Volo.Abp;
using WB.EntrevistaABP;

namespace Acme.BookStore.Authors;

public class ViajeWithPasjaeroAlreadyExistsException : BusinessException
{
    public ViajeWithPasjaeroAlreadyExistsException()
        : base(EntrevistaABPDomainErrorCodes. ViajeWithPasjaeroAlreadyExists)
    {
        WithData("Error","error");
    }
}