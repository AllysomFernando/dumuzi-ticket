using DumuziTickets.Domain.Dto;

namespace DumuziTickets.Application.Gateway;

public interface ITicket
{
    List<TicketDTO> FindAll();
    TicketDTO FindById(int id);
    TicketDTO Create(TicketDTO dto);
    TicketDTO Update(TicketDTO dto);
}
