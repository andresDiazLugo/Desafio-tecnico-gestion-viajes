using System;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;
using WB.EntrevistaABP.Viajes;

namespace WB.EntrevistaABP.Reservas;

public class ReservaManager : DomainService
{
    private readonly IReservaRepository? _reservaRepository;
    public ReservaManager(
       IReservaRepository reservaRepository
        )
    {
        _reservaRepository = reservaRepository;
    }

    public async Task<Reserva> CreateAsync(
        [NotNull] Guid pasajeroId,
        [NotNull] Guid viajeId,
        Boolean coordiandor = false
    )
    {    
        return new Reserva(
            GuidGenerator.Create(),
            coordiandor,
            pasajeroId,
            viajeId
        ); 

    }
}

