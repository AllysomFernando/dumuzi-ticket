using backend.Domain.Assertions;
using backend.Domain.Dto;
using backend.domain.entities;
using backend.Domain.Mappers;
using backend.Domain.Repository;

namespace backend.Domain.UseCase.Ticket;

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
