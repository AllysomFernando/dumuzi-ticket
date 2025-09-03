using DumuziTickets.Domain.Dto;

namespace DumuziTickets.Application.Service.Interfaces;

public interface ITicketService
{
    List<TicketDTO> FindAll();
    TicketDTO? FindById(int id);
    TicketDTO Create(CreateTicketDTO ticket);
    TicketDTO Update(int id, UpdateTicketDTO ticket);
}
