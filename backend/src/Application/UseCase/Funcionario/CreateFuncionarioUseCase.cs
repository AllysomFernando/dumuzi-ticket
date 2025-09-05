using DumuziTickets.Application.DTO;
using DumuziTickets.Application.Dto.Response;
using DumuziTickets.Application.Gateway;
using DumuziTickets.Application.Mappers;
using DumuziTickets.domain.entities;

namespace DumuziTickets.Application.UseCase.Funcionario;

public class CreateFuncionarioAppUseCase
{
    private readonly IFuncionario _funcionarioRepository;

    public CreateFuncionarioAppUseCase(IFuncionario funcionarioRepository)
    {
        _funcionarioRepository = funcionarioRepository;
    }

    public async Task<FuncionarioResponseDTO> Execute(FuncionarioRequestDTO request)
    {
        FuncionarioBO bo = FuncionarioMapper.ToDomain(request);

        bo = await _funcionarioRepository.Create(bo);

        return FuncionarioMapper.ToResponse(bo);
    }

}




