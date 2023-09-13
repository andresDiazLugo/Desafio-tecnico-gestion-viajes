using Volo.Abp.Application.Dtos;

namespace WB.EntrevistaABP.Reservas;

public class GetReservaListDto : PagedAndSortedResultRequestDto{
    public string? Filter {get; set;}
}