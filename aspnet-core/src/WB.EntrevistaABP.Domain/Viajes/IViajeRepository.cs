using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
namespace WB.EntrevistaABP.Viajes;

public interface IViajeRepository : IRepository<Viaje,Guid>
{
    Task<List<Viaje>>GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null
    );
    Task<Viaje> GetViajeAsync(Guid viajeId);
    Task<Viaje> ContainsPasajero(Viaje viaje,Guid pasajeroId);
    // Task<List<Viaje>> GetViajesByPasajeroIdAsync(Guid pasajeroId);
}