using backend.domain.entities;

namespace backend.Domain.Repository;

public interface ITicketRepository : IRepository<TicketBO, int>
{
    IEnumerable<TicketBO> FindByFuncionarioId(int funcionarioId);
    IEnumerable<TicketBO> FindByFuncionarioRange(int id, DateTime dataInicial, DateTime dataFinal);
}
