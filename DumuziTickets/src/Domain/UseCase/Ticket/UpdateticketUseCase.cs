using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class UpdateticketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public UpdateticketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public TicketDTO Execute(TicketDTO dto)
    {
        TicketBO bo = TicketMapper.ToBO(dto);
        bo = _ticketRepository.Update(bo);
        return TicketMapper.ToDTO(bo);
    }
}
