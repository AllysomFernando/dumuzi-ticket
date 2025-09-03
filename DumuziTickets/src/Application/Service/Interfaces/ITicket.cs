using DumuziTickets.Domain.Dto;

namespace DumuziTickets.Application.Service.Interfaces;

public interface ITicketService
{
    List<TicketDTO> FindAll();
    TicketDTO? FindById(int id);
   List<TicketDTO> FindByFuncionarioId(int funcionarioId);
    TicketDTO Create(CreateTicketDTO ticket);
    TicketDTO Update(int id, UpdateTicketDTO ticket);
}
