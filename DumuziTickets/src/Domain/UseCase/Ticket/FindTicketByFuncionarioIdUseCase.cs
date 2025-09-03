using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class FindTicketByFuncioarioIdUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public FindTicketByFuncioarioIdUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(int funcionarioId)
    {
        TicketBO? bo = _ticketRepository.FindByFuncionarioId(funcionarioId);
        Assert.IsNotNull(bo, $"ticket não encontrado pelo funcionarioId{funcionarioId}");
        return TicketMapper.ToDTO(bo);
    }
}
