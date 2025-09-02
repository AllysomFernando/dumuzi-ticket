using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class GetAllTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;

    public GetAllTicketUseCase(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public IEnumerable<TicketDTO> Execute()
    {
        IEnumerable<TicketBO> bo = _ticketRepository.FindAll();

        var ticketsDTO = bo.Select(bo => new TicketDTO
        {
            Id = bo.Id,
            Funcionario = FuncionarioMapper.ToDTO(bo.Funcionario),
            Quantidade = bo.Quantidade,
            Situacao = bo.Situacao,
            UpdatedAt = bo.UpdatedAt,
        });

        return ticketsDTO;
    }
}
