using DumuziTickets.Domain.Dto;
using DumuziTickets.Domain.Mappers;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Funcionario;

public class UpdateFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public UpdateFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public FuncionarioDTO Execute(FuncionarioDTO dto)
    {
        var bo = FuncionarioMapper.ToBO(dto);
        bo.AtualizarData();
        bo = _funcionarioRepository.Update(bo);
        return FuncionarioMapper.ToDTO(bo);
    }
}
