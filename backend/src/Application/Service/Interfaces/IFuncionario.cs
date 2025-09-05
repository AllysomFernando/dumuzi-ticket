using backend.Domain.Dto;

namespace backend.Application.Service.Interfaces;

public interface IFuncionarioService
{
    List<FuncionarioDTO> FindAll();
    FuncionarioDTO? FindById(int id);
    FuncionarioDTO Create(FuncionarioDTO funcionario);
    FuncionarioDTO Update(int id, FuncionarioDTO funcionario);
    FuncionarioDTO Activate(int id);
    FuncionarioDTO Deactivate(int id);
}
