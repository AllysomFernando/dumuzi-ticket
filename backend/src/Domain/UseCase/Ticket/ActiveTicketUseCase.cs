using backend.Domain.Assertions;
using backend.Domain.Dto;
using backend.domain.entities;
using backend.Domain.Mappers;
using backend.Domain.Repository;

namespace backend.Domain.UseCase.Ticket;

public class ActiveTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public ActiveTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(int id)
    {
        TicketBO bo = _ticketRepository.FindById(id);
        Assert.IsNotNull(bo, "Ticket não encontrado");
        bo = _ticketRepository.Activate(id);
        return TicketMapper.ToDTO(bo);
    }
}
