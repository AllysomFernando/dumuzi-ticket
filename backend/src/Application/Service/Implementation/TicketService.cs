using backend.Application.Service.Interfaces;
using backend.Domain.Dto;
using backend.Domain.Repository;
using backend.Domain.UseCase.Ticket;

namespace backend.Application.Service.Implementation;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;
    private readonly IFuncionarioRepository _funcionarioRepository;

    public TicketService(ITicketRepository ticketRepository, IFuncionarioRepository funcionarioRepository)
    {
        _ticketRepository = ticketRepository;
        _funcionarioRepository = funcionarioRepository;
    }

    public List<TicketDTO> FindAll()
    {
        GetAllTicketUseCase useCase = new GetAllTicketUseCase(_ticketRepository);
        return useCase.Execute().ToList();
    }

    public TicketDTO? FindById(int id)
    {
        FindTicketUseCase useCase = new FindTicketUseCase(_ticketRepository);
        return useCase.Execute(id);
    }

    public List<TicketDTO> FindByFuncionarioRange(int funcionarioId, DateTime dataInicial, DateTime dataFinal)
    {
        FindTicketByFuncionarioRange useCase = new FindTicketByFuncionarioRange(_ticketRepository);
        return useCase.Execute(funcionarioId, dataInicial, dataFinal).ToList();
    }

    public List<TicketDTO> FindByFuncionarioId(int funcionarioId)
    {
        FindTicketByFuncioarioIdUseCase useCase = new FindTicketByFuncioarioIdUseCase(_ticketRepository);
        return useCase.Execute(funcionarioId).ToList();
    }

    public TicketDTO Create(CreateTicketDTO ticket)
    {
        CreateTicketUseCase useCase = new CreateTicketUseCase(_ticketRepository, _funcionarioRepository);
        return useCase.Execute(ticket);
    }

    public TicketDTO Update(int id, UpdateTicketDTO ticket)
    {
        UpdateTicketUseCase useCase = new UpdateTicketUseCase(_ticketRepository, _funcionarioRepository);
        return useCase.Execute(id, ticket);
    }

    public TicketDTO Activate(int id)
    {
        ActiveTicketUseCase useCase = new ActiveTicketUseCase(_ticketRepository);
        return useCase.Execute(id);
    }

    public TicketDTO Deactivate(int id)
    {
        DeactivateTicketUseCase useCase = new DeactivateTicketUseCase(_ticketRepository);
        return useCase.Execute(id);
    }

}
