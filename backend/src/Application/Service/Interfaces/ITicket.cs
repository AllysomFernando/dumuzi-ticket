using backend.Domain.Dto;

namespace backend.Application.Service.Interfaces;

public interface ITicketService
{
    List<TicketDTO> FindAll();
    TicketDTO? FindById(int id);
    List<TicketDTO> FindByFuncionarioId(int funcionarioId);
    List<TicketDTO> FindByFuncionarioRange(int funcionarioId, DateTime dataInicial, DateTime dataFinal);
    TicketDTO Create(CreateTicketDTO ticket);
    TicketDTO Update(int id, UpdateTicketDTO ticket);
    TicketDTO Activate(int id);
    TicketDTO Deactivate(int id);
}
