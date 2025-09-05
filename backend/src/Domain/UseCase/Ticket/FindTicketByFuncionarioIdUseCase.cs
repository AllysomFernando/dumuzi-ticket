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

    public IEnumerable<TicketDTO> Execute(int funcionarioId)
    {
        IEnumerable<TicketBO> bo = _ticketRepository.FindByFuncionarioId(funcionarioId);
        Assert.IsNotNull(bo, $"ticket não encontrado pelo funcionarioId{funcionarioId}");

        var ticketsDto = bo.Select(bo => new TicketDTO
        {
            Id = bo.Id,
            Funcionario = FuncionarioMapper.ToDTO(bo.Funcionario),
            Quantidade = bo.Quantidade,
            Situacao = bo.Situacao,
            UpdatedAt = bo.UpdatedAt,
        });
        return ticketsDto;
    }
}
