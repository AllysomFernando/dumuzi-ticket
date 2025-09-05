using DumuziTickets.Domain.Assertions;
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

    public FuncionarioDTO Execute(int id, FuncionarioDTO dto)
    {
        FuncionarioBO bo = _funcionarioRepository.FindById(id);
        Assert.IsNotNull(bo, "Funcionário não encontrado");
        bo.AtualizarFuncionario(FuncionarioMapper.ToBO(dto));
        bo = _funcionarioRepository.Update(id, bo);
        return FuncionarioMapper.ToDTO(bo);
    }
}
