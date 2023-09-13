using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace WB.EntrevistaABP.Reservas;

public interface IReservaAppService : IApplicationService
{
    Task<ReservaDto> GetAsync(Guid id);

    Task<PagedResultDto<ReservaDto>> GetListAsync(GetReservaListDto input);

    Task<ReservaDto> CreateAsync(CreateReservaDto input);

    Task UpdateAsync(Guid id, UpdateReservaDto input);

    Task DeleteAsync(Guid id);
    Task<ListResultDto<PasajeroLookupDto>> GetPasajeroLookupAsync();

    Task<ListResultDto<ViajeLookupDto>> GetViajeLookupAsync();


}