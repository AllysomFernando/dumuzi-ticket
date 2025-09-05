using backend.Domain.Dto;
using backend.domain.entities;
using backend.Domain.Repository;

namespace backend.Domain.UseCase.Funcionario;

public class GetAllFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public GetAllFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public IEnumerable<FuncionarioDTO> Execute()
    {
        IEnumerable<FuncionarioBO> bo = _funcionarioRepository.FindAll();

        var funcionariosDTO = bo.Select(bo => new FuncionarioDTO
        {
            Id = bo.Id,
            Nome = bo.Nome,
            Cpf = bo.Cpf,
            Situacao = bo.Situacao,
            UpdatedAt = bo.UpdatedAt
        });


        return funcionariosDTO;
    }
}
