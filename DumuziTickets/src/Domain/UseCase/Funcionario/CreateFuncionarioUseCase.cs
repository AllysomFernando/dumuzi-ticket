using DumuziTickets.Domain.Assertions;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Domain.Exceptions;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Funcionario;

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
        bo = _funcionarioRepository.Create(bo);

        return FuncionarioMapper.ToDTO(bo);
    }
}
