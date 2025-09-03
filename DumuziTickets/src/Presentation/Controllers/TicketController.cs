using DumuziTickets.Application.Service.Interfaces;
using DumuziTickets.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DumuziTickets.Presentation.Controllers;
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
        try
        {
            List<TicketDTO> res = _ticketService.FindAll();
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todos os tickets");
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<TicketDTO> GetById(int id)
    {
        try
        {
            TicketDTO? res = _ticketService.FindById(id);
            if (res == null)
            {
                return NotFound($"Ticket com ID {id} não encontrado");
            }
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar ticket por ID: {Id}", id);
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }

    [HttpPost]
    public ActionResult<TicketDTO> Create([FromBody] TicketDTO ticket)
    {
        try
        {
            if (ticket == null)
            {
                return BadRequest("Dados do ticket inválidos");
            }

            TicketDTO res = _ticketService.Create(ticket);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar ticket");
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<TicketDTO> Update(int id, [FromBody] TicketDTO ticket)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar ticket com ID: {Id}", id);
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }

    }
}
