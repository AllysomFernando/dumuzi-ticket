using DumuziTickets.Application.DTO;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;

namespace DumuziTickets.Application.Gateway;

public interface IFuncionario
{
    Task<List<FuncionarioBO>> FindAll();
    Task<FuncionarioBO?> FindById(int id);
    Task<FuncionarioBO> Create(FuncionarioBO funcionario);
    Task<FuncionarioBO> Update(FuncionarioBO funcionario);
}
