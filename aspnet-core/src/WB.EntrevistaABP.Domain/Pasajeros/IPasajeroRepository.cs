using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace WB.EntrevistaABP.Pasajeros;

public interface IPasajeroRepository : IRepository<Pasajero,Guid>
{
    Task<List<Pasajero>>GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null
    );
    Task<Pasajero> FindByPasajeroAsync(string nombre,string apellido, string dni, DateTime fecha_de_nacimiento);
}