using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WB.EntrevistaABP.Permissions;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using WB.EntrevistaABP.Pasajeros;
using Acme.BookStore.Viajes;
using Volo.Abp.Users;




namespace WB.EntrevistaABP.Viajes;

// [Authorize(EntrevistaABPPermissions.Viajes.Default)]
public class ViajeAppService: EntrevistaABPAppService,IViajeAppService
{
    private readonly IViajeRepository _viajeRepository;
    private readonly ViajeManager _viajeManager;
    private readonly IRepository<Pasajero,Guid> _pasajeroAppService;
    private readonly ICurrentUser _currentUser;
    public ViajeAppService(
        IViajeRepository viajeRepository,
        ViajeManager viajeManager,
        IRepository<Pasajero,Guid> pasajeroAppService,
        ICurrentUser currentUser
        )
        {
            _viajeRepository = viajeRepository;
            _viajeManager = viajeManager;
            _pasajeroAppService = pasajeroAppService;
            _currentUser = currentUser;

        }
    public async Task<ViajeDto> GetAsync(Guid id)
    {
       var viaje = await _viajeRepository.GetAsync(id);
        return ObjectMapper.Map<Viaje, ViajeDto>(viaje);
    }
    public async Task<PagedResultDto<ViajeDto>> GetListAsync(GetViajeListDto input)
    {
        if (input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Viaje.Origen);
        }
        if (input.Sorting.Equals("Date asc", StringComparison.OrdinalIgnoreCase))
        {
            input.Sorting = nameof(Viaje.Fecha_de_llegada);
        }
        if (input.Sorting.Equals("Date asc", StringComparison.OrdinalIgnoreCase))
        {
            input.Sorting = nameof(Viaje.Fecha_de_salida);
        }


        var viajes = await _viajeRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );
        var totalCount = input.Filter == null
        ? await _viajeRepository.CountAsync()
        : await _viajeRepository.CountAsync();
        return new PagedResultDto<ViajeDto>(
            totalCount,
            ObjectMapper.Map<List<Viaje>, List<ViajeDto>>(viajes)
        );
    }
    [Authorize(EntrevistaABPPermissions.Viajes.Create)]
    public async Task<ViajeDto> CreateAsync(CreateViajeDto input)
    {
    var viaje = await _viajeManager.CreateAsync(
        input.Fecha_de_salida,
        input.Fecha_de_llegada,
        input.Origen,
        input.Destino,
        input.Medio_transporte
    );

    await _viajeRepository.InsertAsync(viaje);

    return ObjectMapper.Map<Viaje, ViajeDto>(viaje);
    }

    [Authorize(EntrevistaABPPermissions.Viajes.Edit)]
    public async Task UpdateAsync(Guid id, UpdateViajeDto input)
    {
    var viaje = await _viajeRepository.GetAsync(id);
    
    viaje.Origen = input.Origen;
    viaje.Destino = input.Destino;
    viaje.Fecha_de_salida = input.Fecha_de_salida;
    viaje.Fecha_de_llegada = input.Fecha_de_llegada;
    viaje.Medio_transporte = input.Medio_transporte;

    await _viajeRepository.UpdateAsync(viaje);
    }

    [Authorize(EntrevistaABPPermissions.Viajes.Delete)]
    public async Task DeleteAsync(Guid id)
    {
    var existingViaje = await _viajeRepository.GetViajeAsync(id);
    if(existingViaje != null){
        if(existingViaje.ListaUsuarios.Count() > 0)
        {
            throw new ViajeContainsPasjaerosException();
        }
    }
    await _viajeRepository.DeleteAsync(id);
    }

}

