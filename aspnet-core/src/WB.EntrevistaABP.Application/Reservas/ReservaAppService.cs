using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using WB.EntrevistaABP.Pasajeros;
using WB.EntrevistaABP.Permissions;
using WB.EntrevistaABP.Viajes;

namespace WB.EntrevistaABP.Reservas;

[Authorize(EntrevistaABPPermissions.Reservas.Default)]
public class ReservaAppService: EntrevistaABPAppService,IReservaAppService
{

    private readonly IReservaRepository _reservaRepository;
    private readonly ReservaManager _reservaManager;
    private readonly IPasajeroRepository _pasajeroRepository;
    private readonly IRepository<Viaje,Guid> _viajeRepository;
    private readonly ViajeManager _viajeManager;

    // private readonly UserManager<IdentityUser> _userManager;

    public ReservaAppService(
        IReservaRepository reservaRepository,
        ReservaManager reservaManager,
        IPasajeroRepository pasajeroRespository,
        IRepository<Viaje,Guid> viajeRepository,
        ViajeManager viajeManager
    ){
        _reservaManager = reservaManager;
        _reservaRepository = reservaRepository;
        _pasajeroRepository = pasajeroRespository;
        _viajeRepository = viajeRepository;
        _viajeManager = viajeManager;
        
    }


    public async Task<ReservaDto> GetAsync(Guid id)
    {
        if (_reservaRepository == null)
        {

        throw new ArgumentNullException("_pasajeroRepository");
        }

        var reserva = await _reservaRepository.GetAsync(id);

        if (reserva == null)
        {
        throw new ArgumentNullException("reserva");
        }
        return ObjectMapper.Map<Reserva, ReservaDto>(reserva);
    }

    public async Task<PagedResultDto<ReservaDto>> GetListAsync(GetReservaListDto input)
    {
        var queryable = await _reservaRepository.WithDetailsAsync(x => x.Viaje, x => x.Pasajero); // Asegúrate de usar WithDetailsAsync en lugar de WithDetails
        var reservas = await AsyncExecuter.ToListAsync(queryable);
        var reservaDtos = ObjectMapper.Map<List<Reserva>, List<ReservaDto>>(reservas);
        var totalCount = reservaDtos.Count;
        var items = reservaDtos.ToList();

        return new PagedResultDto<ReservaDto>(totalCount,items);


    }
    [Authorize(EntrevistaABPPermissions.Reservas.Create)]
    public async Task<ReservaDto> CreateAsync(CreateReservaDto input)
    {

        var pasajeroId = input.PasajeroId ?? Guid.Empty; 
        var viajeId = input.VIajeId ?? Guid.Empty;       
        var coordiandor = input.Coordinador ?? false;
        var comprobateExistsPasajeroViaje = await _viajeManager.AddPasajeros(pasajeroId,viajeId);
        if(comprobateExistsPasajeroViaje != null)
        {
            
            comprobateExistsPasajeroViaje.ListaUsuarios.Add(pasajeroId);
        }
        var reserva = await _reservaManager.CreateAsync(
            pasajeroId,
            viajeId,
            coordiandor
        );
        await _reservaRepository.InsertAsync(reserva);
        return ObjectMapper.Map<Reserva, ReservaDto>(reserva);
    }

    [Authorize(EntrevistaABPPermissions.Reservas.Edit)]
    public async Task UpdateAsync(Guid id, UpdateReservaDto input)
    {
        var reserva = await _reservaRepository.GetAsync(id);
    
        reserva.Coordinador = input.Coordinador ?? false;
        reserva.PasajeroId = input.PasajeroId  ?? Guid.Empty;
        reserva.ViajeId = input.ViajeId  ?? Guid.Empty;

        await _reservaRepository.UpdateAsync(reserva);
    }
    [Authorize(EntrevistaABPPermissions.Reservas.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var reserva = await _reservaRepository.GetAsync(id);
        var viaje = await _viajeRepository.GetAsync(reserva.ViajeId);
        viaje.ListaUsuarios = new List<Guid>{};
        await _viajeRepository.UpdateAsync(viaje);
        await _reservaRepository.DeleteAsync(id);
    }

    public async Task<ListResultDto<Reservas.PasajeroLookupDto>> GetPasajeroLookupAsync()
    {
    var pasajeros = await _pasajeroRepository.GetListAsync();
    return new ListResultDto<PasajeroLookupDto>(
        ObjectMapper.Map<List<Pasajero>, List<PasajeroLookupDto>>(pasajeros)
    );
    }

  public async Task<ListResultDto<ViajeLookupDto>> GetViajeLookupAsync()
    {
    var viajes = await _viajeRepository.GetListAsync();
    return new ListResultDto<ViajeLookupDto>(
        ObjectMapper.Map<List<Viaje>, List<ViajeLookupDto>>(viajes)
    );
    }
}