using backend.Domain.Assertions;
using backend.Domain.Dto;
using backend.domain.entities;
using backend.Domain.Mappers;
using backend.Domain.Repository;
using Assert = backend.Domain.Assertions.Assert;

namespace backend.Domain.UseCase.Funcionario;

public class DeactivateFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public DeactivateFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public FuncionarioDTO Execute(int id)
    {
        FuncionarioBO bo = _funcionarioRepository.FindById(id);
        Assert.IsNotNull(bo, "Funcionário não encontrado");
        bo = _funcionarioRepository.Deactivate(id);
        return FuncionarioMapper.ToDTO(bo);
    }
}
