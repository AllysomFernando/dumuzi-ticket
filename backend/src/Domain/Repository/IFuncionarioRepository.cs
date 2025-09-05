using backend.domain.entities;
using backend.Domain.Dto;

namespace backend.Domain.Repository;

public interface IFuncionarioRepository : IRepository<FuncionarioBO, int>
{
    FuncionarioBO FindByCPF(string cpf);
}
