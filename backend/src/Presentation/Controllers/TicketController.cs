using backend.Application.Service.Interfaces;
using backend.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TicketController : Controller
{
    private readonly ILogger<TicketController> _logger;
    private readonly ITicketService _ticketService;

    public TicketController(ILogger<TicketController> logger, ITicketService ticketService)
    {
        _logger = logger;
        _ticketService = ticketService;
    }

    [HttpGet]
    public ActionResult<List<TicketDTO>> Get()
    {

            List<TicketDTO> res = _ticketService.FindAll();
            return Ok(res);
    }

    [HttpGet("{id}")]
    public ActionResult<TicketDTO> GetById(int id)
    {

            TicketDTO? res = _ticketService.FindById(id);
            if (res == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado");
            }
            return Ok(res);

    }

    [HttpGet("funcionario/{id}")]
    public ActionResult<TicketDTO> GetByFuncionarioId(int id)
    {

            List<TicketDTO> res = _ticketService.FindByFuncionarioId(id);
            if (res == null)
            {
                return NotFound($"Ticket para o funcionário com ID {id} não encontrado");
            }
            return Ok(res);
    }

    [HttpGet("funcionario/{id}/{dataInicial}/{dataFinal}")]
    public ActionResult<TicketDTO> GetByFuncionarioIdRange(int id, DateTime dataInicial, DateTime dataFinal)
    {
        List<TicketDTO> res = _ticketService.FindByFuncionarioRange(id, dataInicial, dataFinal);
        if (res == null)
        {
            return NotFound($"Ticket para o funcionário com ID {id} no intervalo de {dataInicial} a {dataFinal} não encontrado");
        }
        return Ok(res);
    }

    [HttpPost]
    public ActionResult<TicketDTO> Create([FromBody] CreateTicketDTO ticket)
    {

            if (ticket == null)
            {
                return BadRequest("Dados do ticket inválidos");
            }

            TicketDTO res = _ticketService.Create(ticket);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);

    }

    [HttpPut("{id}")]
    public ActionResult<TicketDTO> Update(int id, [FromBody] UpdateTicketDTO ticket)
    {

            if (ticket == null)
            {
                return BadRequest("Dados do ticket inválidos");
            }
            TicketDTO res = _ticketService.Update(id, ticket);
            if (res == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado");
            }

            return Ok(res);
    }
    [HttpPatch("deactivate/{id}")]
    public ActionResult<TicketDTO> Deactivate(int id)
    {
        if (id == null)
        {
            return BadRequest("O ID do funcionário não pode ser nulo");
        }
        TicketDTO res = _ticketService.Deactivate(id);

        if (res == null)
        {
            return NotFound($"Funcionário com ID {id} não encontrado");
        }

        return Ok(res);
    }

    [HttpPatch("activate/{id}")]
    public ActionResult<TicketDTO> Activate(int id)
    {
        if (id == null)
        {
            return BadRequest("O ID do ticket não pode ser nulo");
        }

        TicketDTO res = _ticketService.Activate(id);
        if (res == null)
        {
            return NotFound($"Ticket com ID {id} não encontrado");
        }

        return Ok(res);
    }

}
