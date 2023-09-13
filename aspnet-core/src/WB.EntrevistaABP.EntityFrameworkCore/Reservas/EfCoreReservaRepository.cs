using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WB.EntrevistaABP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;


namespace WB.EntrevistaABP.Reservas;

public class EfCoreReservaRepository : EfCoreRepository<EntrevistaABPDbContext, Reserva, Guid>, IReservaRepository
{
    public EfCoreReservaRepository(IDbContextProvider<EntrevistaABPDbContext>dbContextProvider): base(dbContextProvider)
    {}
    public async Task<List<Reserva>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null
    )
    {
        var dbSet = await GetDbSetAsync();

        var query = await dbSet
            .Include(r => r.Pasajero) // Incluir datos relacionados de Pasajero
            .Include(r => r.Viaje)    // Incluir datos relacionados de Viaje
            .ToListAsync();

    return query; 
    }
    public async Task<Reserva> FindByReservaAsync(Guid id)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(reserva => reserva.Id == id );
    }

}