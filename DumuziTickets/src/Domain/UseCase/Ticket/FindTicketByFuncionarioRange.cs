using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class FindTicketByFuncionarioRange
{
    private readonly ITicketRepository _ticketRepository;

    public FindTicketByFuncionarioRange(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
    }

    public IEnumerable<TicketDTO> Execute(int funcionarioId, DateTime dataInicial, DateTime dataFinal)
    {
        Assert.IsGreaterThan(dataFinal, dataInicial, "Data final deve ser maior que data inicial");
        IEnumerable<TicketBO> bo = _ticketRepository.FindByFuncionarioRange(funcionarioId, dataInicial, dataFinal);
        Assert.IsNotNull(bo, $"ticket não encontrado pelo funcionarioId: {funcionarioId} e dataInicial: {dataInicial} e dataFinal: {dataFinal}");
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
