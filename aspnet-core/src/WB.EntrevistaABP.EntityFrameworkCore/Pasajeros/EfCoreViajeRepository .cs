using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WB.EntrevistaABP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace WB.EntrevistaABP.Pasajeros;

public class EfCorePasajeroRepository : EfCoreRepository<EntrevistaABPDbContext, Pasajero, Guid>, IPasajeroRepository
{
    public EfCorePasajeroRepository(IDbContextProvider<EntrevistaABPDbContext>dbContextProvider): base(dbContextProvider)
    {}
    public async Task<List<Pasajero>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null
    )
    {
        var query = await GetDbSetAsync();
        return await query
        .WhereIf(
            !filter.IsNullOrWhiteSpace(),
            pasajero => pasajero.DNI.Contains(filter)
            )
            .OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync();
    }
    public async Task<List<Pasajero>> GetListPasajeriAsyncForViaje()
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.ToListAsync();  
    }
    public async Task<Pasajero?> FindByPasajeroAsync(string nombre,string apellido, string dni, DateTime fecha_de_nacimiento)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(p =>
        p.Nombre == nombre &&
        p.Apellido == apellido &&
        p.DNI == dni &&
        p.Fecha_de_nacimiento == fecha_de_nacimiento &&
        (p.IsDeleted == true || p.IsDeleted == false));

    }

}