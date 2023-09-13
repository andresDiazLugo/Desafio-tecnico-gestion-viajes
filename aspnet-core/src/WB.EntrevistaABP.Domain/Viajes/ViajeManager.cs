using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;

namespace WB.EntrevistaABP.Viajes;

public class ViajeManager : DomainService
{
    private readonly IViajeRepository _viajeRepository;
    public ViajeManager(IViajeRepository viajeRepository)
    {
        _viajeRepository = viajeRepository;
    }

    public async Task<Viaje> CreateAsync(
        [NotNull] DateTime fecha_de_salida,
        [NotNull] DateTime fecha_de_llegada,
        [NotNull] string origen,
        [NotNull]  string destino,
        [NotNull]  ViajeType medio_transporte)
        {        
        
        var pasajeros = new List<Guid>{};
         return  new Viaje(
            GuidGenerator.Create(),
            fecha_de_salida,
            fecha_de_llegada,
            origen,
            destino,
            medio_transporte,
            pasajeros
            );
        }
    public async Task<Viaje?> AddPasajeros(
        Guid pasajeroId,
        Guid viajeId
    )
    {
        var existingViaje = await _viajeRepository.GetViajeAsync(viajeId);
        if(existingViaje != null){
            var comprobateViajeContainsPasajero = await _viajeRepository.ContainsPasajero(existingViaje,pasajeroId);
    
            if(comprobateViajeContainsPasajero != null)
            {
                return existingViaje;
            }
            else
            {
                throw new ViajeWithPasjaeroAlreadyExistsException();
            }
        } 
        return null;
        
    }

}