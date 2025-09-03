using DumuziTickets.Domain.Dto;

namespace DumuziTickets.Application.Service.Interfaces;

public interface IFuncionarioService
{
    List<FuncionarioDTO> FindAll();
    FuncionarioDTO? FindById(int id);
    FuncionarioDTO Create(FuncionarioDTO funcionario);
    FuncionarioDTO Update(int id, FuncionarioDTO funcionario);
}
