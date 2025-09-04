using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class DeactivateTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public DeactivateTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(int id)
    {
        TicketBO bo = _ticketRepository.FindById(id);
        Assert.IsNotNull(bo, "Ticket não encontrado");
        bo = _ticketRepository.Deactivate(id);
        return TicketMapper.ToDTO(bo);
    }
}
