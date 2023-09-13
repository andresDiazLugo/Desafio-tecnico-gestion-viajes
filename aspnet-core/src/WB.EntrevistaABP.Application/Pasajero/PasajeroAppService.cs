using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using WB.EntrevistaABP.Permissions;

namespace WB.EntrevistaABP.Pasajeros;

[Authorize(EntrevistaABPPermissions.Pasajeros.Default)]
public class PasajeroAppService: EntrevistaABPAppService,IPasajeroAppService
{

    private readonly IPasajeroRepository _pasajeroRepository;
    private readonly PasajeroManager _pasajeroManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IdentityUserManager _identityUserManager;

    public PasajeroAppService(
        IPasajeroRepository pasajeroRepository,
        PasajeroManager pasajeroManager,
        UserManager<IdentityUser> userManager,
        IdentityUserManager identityUserManager
    ){
        _pasajeroManager = pasajeroManager;
        _pasajeroRepository = pasajeroRepository;
        _userManager = userManager;
        _identityUserManager = identityUserManager;
    }


    public async Task<PasajeroDto> GetAsync(Guid id)
    {
        if (_pasajeroRepository == null)
        {

        throw new ArgumentNullException("_pasajeroRepository");
        }

        var pasajero = await _pasajeroRepository.GetAsync(id);

        if (pasajero == null)
        {
        throw new ArgumentNullException("pasajero");
        }
        return ObjectMapper.Map<Pasajero, PasajeroDto>(pasajero);
    }

    public async Task<PagedResultDto<PasajeroDto>> GetListAsync(GetPasajeroListDto input)
    {
        if(input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Pasajero.Nombre);
        }
         if(input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Pasajero.Apellido);
        }
         if(input.Sorting.IsNullOrWhiteSpace())
        {
            input.Sorting = nameof(Pasajero.DNI);
        }
        var pasajeros = await _pasajeroRepository.GetListAsync(
            input.SkipCount,
            input.MaxResultCount,
            input.Sorting,
            input.Filter
        );
        
        var totalCount = input.Filter == null
        ? await _pasajeroRepository.CountAsync()
        : await  _pasajeroRepository.CountAsync();
        return new PagedResultDto<PasajeroDto>(
            totalCount,
            ObjectMapper.Map<List<Pasajero>, List<PasajeroDto>>(pasajeros)
        );
    }
    [Authorize(EntrevistaABPPermissions.Pasajeros.Create)]
    public async Task<PasajeroDto> CreateAsync(CreatePasajeroDto input)
    {
        var pasajero = await _pasajeroManager.CreateAsync(
            input.Nombre,
            input.Apellido,
            input.DNI,
            input.Email,
            input.Fecha_de_nacimiento
        );
        
        var usuario = await _identityUserManager.FindByIdAsync(pasajero.UserId.ToString());
        if(usuario != null){
            usuario.IsDeleted = false;
            await _identityUserManager.UpdateAsync(usuario);
        }
        await _pasajeroRepository.InsertAsync(pasajero);
        return ObjectMapper.Map<Pasajero, PasajeroDto>(pasajero);
    
    }

    [Authorize(EntrevistaABPPermissions.Pasajeros.Edit)]
    public async Task UpdateAsync(Guid id, UpdatePasajeroDto input)
    {
        var pasajero = await _pasajeroRepository.GetAsync(id);
        pasajero.Nombre = input.Nombre;
        pasajero.Apellido = input.Apellido;
        pasajero.Fecha_de_nacimiento = input.Fecha_de_nacimiento;
        pasajero.Email = input.DNI;

        await _pasajeroRepository.UpdateAsync(pasajero);
    }
    [Authorize(EntrevistaABPPermissions.Pasajeros.Delete)]
    public async Task DeleteAsync(Guid id)
    {
        var searchingUser = await _pasajeroRepository.GetAsync(id);
        var usuario = await _identityUserManager.FindByIdAsync(searchingUser.UserId.ToString());
        if (usuario != null){
            await _identityUserManager.DeleteAsync(usuario);
        }
        await _pasajeroRepository.DeleteAsync(id);
    }

}