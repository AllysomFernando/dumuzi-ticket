using DumuziTickets.Domain.Repository;

namespace DumuziTickets.Domain.UseCase.Funcionario;

public class UpdateFuncionarioUseCase
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public UpdateFuncionarioUseCase(IFuncionarioRepository funcionarioRepository)
    {
        
    }
}