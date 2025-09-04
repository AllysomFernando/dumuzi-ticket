using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;
using Assert = DumuziTickets.Domain.Assertions.Assert;

namespace DumuziTickets.Domain.UseCase.Funcionario;

public class ActiveFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public ActiveFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public FuncionarioDTO Execute(int id)
    {
        FuncionarioBO bo = _funcionarioRepository.FindById(id);
        Assert.IsNotNull(bo, "Funcionário não encontrado");
        bo = _funcionarioRepository.Activate(id);
        return FuncionarioMapper.ToDTO(bo);
    }
}
