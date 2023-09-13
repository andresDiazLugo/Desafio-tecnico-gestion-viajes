using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;


namespace WB.EntrevistaABP.Reservas;

public interface IReservaRepository : IRepository<Reserva,Guid>
{
    Task<List<Reserva>>GetListAsync(
        int skipCount,
        int maxResultCount,
        string sorting,
        string? filter = null
    );
     Task<Reserva> FindByReservaAsync(Guid id);

}