using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Pasajeros;

public class GetPasajeroListDto : PagedAndSortedResultRequestDto{
    public string? Filter {get; set;}
}