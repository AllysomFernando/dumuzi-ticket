using DumuziTickets.domain;
using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class UpdateTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;

    public UpdateTicketUseCase(ITicketRepository ticketRepository, IFuncionarioRepository funcionarioRepository)
    {
        _ticketRepository = ticketRepository;
        _funcionarioRepository = funcionarioRepository;
    }

    public TicketDTO Execute(int id, UpdateTicketDTO dto)
    {
        FuncionarioBO funcionarioBo = _funcionarioRepository
            .FindById(dto.FuncionarioId);
        Assert.IsNotNull(funcionarioBo, "Funcionário não encontrado");

        var bo = new TicketBO(
            id: id,
            quantidade: dto.Quantidade,
            funcionario: funcionarioBo,
            situacao: dto.Situacao,
            updatedAt: DateTime.UtcNow
        );
        bo = _ticketRepository.Update(id, bo);
        return TicketMapper.ToDTO(bo);
    }
}
