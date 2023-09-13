using Volo.Abp;


namespace WB.EntrevistaABP.Pasajeros;

public class EmailExistingException : BusinessException
{
    public EmailExistingException(string email)
        : base(EntrevistaABPDomainErrorCodes.EmailAlreadyExists)
    {
        WithData("EMAIL", email);
    }
}