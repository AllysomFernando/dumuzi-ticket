using backend.Domain.Dto;
using backend.domain.entities;
using backend.Domain.Mappers;
using backend.Domain.Repository;

namespace backend.Domain.UseCase.Ticket;

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
