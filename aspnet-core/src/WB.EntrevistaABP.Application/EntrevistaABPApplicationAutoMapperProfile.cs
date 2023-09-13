using AutoMapper;
using WB.EntrevistaABP.Pasajeros;
using WB.EntrevistaABP.Reservas;
using WB.EntrevistaABP.Viajes;

namespace WB.EntrevistaABP;

public class EntrevistaABPApplicationAutoMapperProfile : Profile
{
    public EntrevistaABPApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Pasajero, PasajeroDto>();
        CreateMap<Viaje,ViajeDto>();
        CreateMap<Reserva,ReservaDto>();
        CreateMap<Pasajero,PasajeroLookupDto>();
        CreateMap<Viaje, ViajeLookupDto>();
    }
}
