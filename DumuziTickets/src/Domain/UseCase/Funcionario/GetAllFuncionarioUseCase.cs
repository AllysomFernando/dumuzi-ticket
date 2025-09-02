using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Funcionario;

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
            CreatedAt = bo.CreatedAt,
            UpdatedAt = bo.UpdatedAt
        });


        return funcionariosDTO;
    }
}
