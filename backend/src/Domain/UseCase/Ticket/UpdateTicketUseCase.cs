using backend.Domain.Assertions;
using backend.Domain.Dto;
using backend.domain.entities;
using backend.Domain.Mappers;
using backend.Domain.Repository;
using backend.domain;

namespace backend.Domain.UseCase.Ticket;

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
        TicketBO existingTicket = _ticketRepository
            .FindById(id);
        Assert.IsNotNull(existingTicket, "Ticket não encontrado");
        FuncionarioBO funcionarioBo = _funcionarioRepository
            .FindById(dto.FuncionarioId);
        Assert.IsNotNull(funcionarioBo, "Funcionário não encontrado");

        var bo = new TicketBO(
            id: id,
            quantidade: dto.Quantidade,
            funcionario: funcionarioBo,
            situacao: dto.Situacao,
            updatedAt: existingTicket.UpdatedAt
        );
        bo = _ticketRepository.Update(id, bo);
        return TicketMapper.ToDTO(bo);
    }
}
