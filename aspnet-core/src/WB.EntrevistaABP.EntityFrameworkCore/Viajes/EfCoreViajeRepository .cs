using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using WB.EntrevistaABP.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Users;
using WB.EntrevistaABP.Pasajeros;
using Volo.Abp.Domain.Repositories;


namespace WB.EntrevistaABP.Viajes;

public class EfCoreViajeRepository : EfCoreRepository<EntrevistaABPDbContext, Viaje, Guid>, IViajeRepository
{
    private readonly IPasajeroRepository _pasajeroRepository;
    private readonly ICurrentUser _currentUser;
   
   public EfCoreViajeRepository(
    IDbContextProvider<EntrevistaABPDbContext>dbContextProvider,
    ICurrentUser currentUser,
    IPasajeroRepository pasajeroRepository
    ): base(dbContextProvider)
    {
        _currentUser = currentUser;
        _pasajeroRepository= pasajeroRepository;
    }

    public async Task<List<Viaje>> GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null)
    {

        var date_exit ="";
        var date = DateTime.Now;
        var date_current = DateTime.Now;
        if( filter != null){
            date_exit = filter;
            date = DateTime.Parse(date_exit);

        }
        var query = await  GetDbSetAsync();
        if(_currentUser.IsInRole("admin"))
        {
            return await query
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                viaje => viaje.Fecha_de_salida >= date && viaje.Fecha_de_salida <= date_current
                )
                .OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount)
                .ToListAsync();
        }
        else{
            var userId = _currentUser.Id ?? Guid.Empty;
            var pasajero = await _pasajeroRepository.FirstOrDefaultAsync(p => p.UserId == userId);
            if(filter != null)
            {
                return await query.Where(
                            viaje => viaje.ListaUsuarios.Contains(pasajero.Id) && viaje.Fecha_de_salida >= date && viaje.Fecha_de_salida <= date
                            )
                            .OrderBy(sorting)
                            .Skip(skipCount)
                            .Take(maxResultCount)
                            .ToListAsync();
            }
            else
            {
                return await query.Where(
                            viaje => viaje.ListaUsuarios.Contains(pasajero.Id)
                            )
                            .OrderBy(sorting)
                            .Skip(skipCount)
                            .Take(maxResultCount)
                            .ToListAsync();
            }
      
        }
    
    }
    public async Task<Viaje> GetViajeAsync(Guid viajeId)
    {
        var dbSet = await GetDbSetAsync();
        return await dbSet.FirstOrDefaultAsync(viaje => viaje.Id == viajeId);
    }

    public async Task<Viaje?> ContainsPasajero(Viaje viaje,Guid pasajeroId)
    {
        var verifyPasajero = viaje.ListaUsuarios.Contains(pasajeroId);
        if(verifyPasajero){
        return null;    
        }
        return viaje;
    }


}