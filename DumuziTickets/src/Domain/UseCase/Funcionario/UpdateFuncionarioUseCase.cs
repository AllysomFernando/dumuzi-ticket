using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Exceptions;
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
        FuncionarioBO bo = _funcionarioRepository.FindById(dto.Id);

        if (bo == null)
        {
            throw new Exception("Funcionario não encontrado.");
        }

        if (bo.Cpf != dto.Cpf)
        {
            var exists = _funcionarioRepository.FindByCPF(dto.Cpf);
            if (exists != null)
            {
                throw new BusinessExecption("Não é possivel atualizar com esse CPF, pois já consta em nosso sistema.");
            }
        }

        bo.AtualizarFuncionario(FuncionarioMapper.ToBO(dto));
        bo = _funcionarioRepository.Update(bo);
        return FuncionarioMapper.ToDTO(bo);
    }
}
