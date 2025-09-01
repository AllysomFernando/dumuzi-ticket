using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class CreateTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;

    public CreateTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(TicketDTO dto)
    {
        TicketBO bo = TicketMapper.ToBo(dto);
        bo = _ticketRepository.Create(bo);

        return TicketMapper.ToDto(bo);
    }

    private void Validate(TicketDTO dto)
    {

    }
}
