using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class FindTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public FindTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(int ticketId)
    {
        TicketBO? bo = _ticketRepository.FindById(ticketId);
        Assert.IsNotNull(bo, "ticket não encontrado");
        return TicketMapper.ToDTO(bo);
    }
}
