using DumuziTickets.Domain.Dto;

namespace DumuziTickets.Application.Gateway;

public interface IFuncionario
{
    List<FuncionarioDTO> FindAll();
    FuncionarioDTO FindById(int id);
    FuncionarioDTO Create(FuncionarioDTO dto);
    FuncionarioDTO Update(FuncionarioDTO dto);
}
