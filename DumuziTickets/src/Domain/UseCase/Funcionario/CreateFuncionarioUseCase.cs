using DumuziTickets.Domain.Dto;
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
        bo = _funcionarioRepository.Create(bo);

        return FuncionarioMapper.ToDTO(bo);
    }
}
