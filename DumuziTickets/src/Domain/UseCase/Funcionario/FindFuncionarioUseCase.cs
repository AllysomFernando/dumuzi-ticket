using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Funcionario;

public class FindFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FindFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public FuncionarioDTO Execute(int funcionarioId)
    {
        var bo = _funcionarioRepository.FindById(funcionarioId);
        Assert.IsNull(bo, "Funcionario não encontrado");
        return FuncionarioMapper.ToDTO(bo);
    }
}
