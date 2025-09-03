using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class UpdateTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public UpdateTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(int id, TicketDTO dto)
    {
        TicketBO bo = TicketMapper.ToBO(dto);
        bo = _ticketRepository.Update(id, bo);
        return TicketMapper.ToDTO(bo);
    }
}
