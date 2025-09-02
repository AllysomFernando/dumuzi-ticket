using DumuziTickets.Application.Service.Interfaces;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Domain.UseCase.Ticket;

namespace DumuziTickets.Application.Service.Implementation;

public class TicketService : ITicketService
{
    private readonly ITicketRepository _ticketRepository;

    public TicketService(ITicketRepository ticketRepository)
    {
        _ticketRepository = ticketRepository;
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

    public TicketDTO Create(TicketDTO ticket)
    {
        CreateTicketUseCase useCase = new CreateTicketUseCase(_ticketRepository);
        return useCase.Execute(ticket);
    }

    public TicketDTO Update(TicketDTO ticket)
    {
        UpdateTicketUseCase useCase = new UpdateTicketUseCase(_ticketRepository);
        return useCase.Execute(ticket);
    }

}
