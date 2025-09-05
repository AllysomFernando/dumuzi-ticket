using backend.Domain.Assertions;
using backend.Domain.Dto;
using backend.Domain.Mappers;
using backend.Domain.Repository;

namespace backend.Domain.UseCase.Funcionario;

public class CreateFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public CreateFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public FuncionarioDTO Execute(FuncionarioDTO dto)
    {
        var bo = FuncionarioMapper.ToBO(dto);
        bo.AtualizarData();
        var exists = _funcionarioRepository.FindByCPF(bo.Cpf);
        Assert.IsNull(exists, "Não é possivel cadastrar com esse CPF, pois já consta em nosso sistema.");
        bo.Validate("Funcionário");
        bo = _funcionarioRepository.Create(bo);

        return FuncionarioMapper.ToDTO(bo);
    }
}
