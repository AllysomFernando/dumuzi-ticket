using DumuziTickets.Application.Service.Interfaces;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Domain.UseCase.Ticket;

namespace DumuziTickets.Application.Service.Implementation;

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

}
