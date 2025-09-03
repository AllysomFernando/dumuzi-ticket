using DumuziTickets.domain;
using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Ticket;

public class CreateTicketUseCase
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;

    public CreateTicketUseCase(ITicketRepository ticketRepository, IFuncionarioRepository funcionarioRepository)
    {
        _ticketRepository = ticketRepository;
        _funcionarioRepository = funcionarioRepository;
    }

    public TicketDTO Execute(CreateTicketDTO dto)
    {
        FuncionarioBO funcionarioBo = _funcionarioRepository.FindById(dto.FuncionarioId);
        Assert.IsNotNull(funcionarioBo, "Funcionário não encontrado");

        var bo = new TicketBO(
            id: 0,
            quantidade: dto.Quantidade,
            funcionario: funcionarioBo,
            situacao: EnumSituacao.A,
            updatedAt: DateTime.Now
        );

        bo.Validate("Ticket");
        bo = _ticketRepository.Create(bo);
        return TicketMapper.ToDTO(bo);
    }
}
