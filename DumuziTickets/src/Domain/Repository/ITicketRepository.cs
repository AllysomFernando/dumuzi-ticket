using DumuziTickets.domain.entities;

namespace DumuziTickets.Domain.Repository;

public interface ITicketRepository : IRepository<TicketBO, int>
{
    TicketBO? FindByFuncionarioId(int funcionarioId);
}
