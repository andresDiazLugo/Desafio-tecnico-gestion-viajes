using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Viajes;

public class GetViajeListDto : PagedAndSortedResultRequestDto{
    public string? Filter {get; set;}
}