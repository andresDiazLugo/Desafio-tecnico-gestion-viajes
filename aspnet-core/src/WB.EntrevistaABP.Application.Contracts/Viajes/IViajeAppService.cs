using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using WB.EntrevistaABP.Viajes;

namespace Acme.BookStore.Viajes;

public interface IViajeAppService : IApplicationService
{
    Task<ViajeDto> GetAsync(Guid id);

    Task<PagedResultDto<ViajeDto>> GetListAsync(GetViajeListDto input);

    Task<ViajeDto> CreateAsync(CreateViajeDto input);

    Task UpdateAsync(Guid id, UpdateViajeDto input);

    Task DeleteAsync(Guid id);
}
