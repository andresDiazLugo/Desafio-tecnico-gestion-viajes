using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;


namespace WB.EntrevistaABP.Pasajeros;

public interface IPasajeroAppService : IApplicationService
{
    Task<PasajeroDto> GetAsync(Guid id);

    Task<PagedResultDto<PasajeroDto>> GetListAsync(GetPasajeroListDto input);

    Task<PasajeroDto> CreateAsync(CreatePasajeroDto input);

    Task UpdateAsync(Guid id, UpdatePasajeroDto input);

    Task DeleteAsync(Guid id);

}
