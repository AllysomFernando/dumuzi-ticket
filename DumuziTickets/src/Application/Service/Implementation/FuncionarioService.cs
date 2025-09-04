using DumuziTickets.Application.Service.Interfaces;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Domain.UseCase.Funcionario;

namespace DumuziTickets.Application.Service.Implementation;

public class FuncionarioService : IFuncionarioService
{
    private readonly IFuncionarioRepository _funcionarioRepository;

    public FuncionarioService(IFuncionarioRepository funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public List<FuncionarioDTO> FindAll()
    {
        GetAllFuncionarioUseCase useCase = new GetAllFuncionarioUseCase(_funcionarioRepository);
        return useCase.Execute().ToList();
    }

    public FuncionarioDTO? FindById(int id)
    {
        FindFuncionarioUseCase useCase = new FindFuncionarioUseCase(_funcionarioRepository);
        return useCase.Execute(id);
    }

    public FuncionarioDTO Create(FuncionarioDTO funcionario)
    {
        CreateFuncionarioUseCase useCase = new CreateFuncionarioUseCase(_funcionarioRepository);
        return useCase.Execute(funcionario);
    }

    public FuncionarioDTO Update(int id, FuncionarioDTO funcionario)
    {
        UpdateFuncionarioUseCase useCase = new UpdateFuncionarioUseCase(_funcionarioRepository);
        return useCase.Execute(id, funcionario);
    }
    public FuncionarioDTO Activate(int id)
    {
        ActiveFuncionarioUseCase useCase = new ActiveFuncionarioUseCase(_funcionarioRepository);
        return useCase.Execute(id);
    }

    public FuncionarioDTO Deactivate(int id)
    {
        DeactivateFuncionarioUseCase useCase = new DeactivateFuncionarioUseCase(_funcionarioRepository);
        return useCase.Execute(id);
    }
}
