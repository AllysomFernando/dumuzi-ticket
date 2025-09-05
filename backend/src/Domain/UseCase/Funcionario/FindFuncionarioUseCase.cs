using backend.Domain.Assertions;
using backend.Domain.Dto;
using backend.Domain.Mappers;
using backend.Domain.Repository;

namespace backend.Domain.UseCase.Funcionario;

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
        Assert.IsNotNull(bo, "Funcionario não encontrado");
        return FuncionarioMapper.ToDTO(bo);
    }
}
