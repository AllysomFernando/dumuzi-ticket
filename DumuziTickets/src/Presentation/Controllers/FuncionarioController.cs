using DumuziTickets.Application.Service.Interfaces;
using DumuziTickets.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace DumuziTickets.Presentation.Controllers;
[ApiController]
[Route("api/[controller]")]
public class FuncionarioController : Controller
{
    private readonly ILogger<FuncionarioController> _logger;
    private readonly IFuncionarioService _funcionarioService;

    public FuncionarioController(ILogger<FuncionarioController> logger, IFuncionarioService funcionarioService)
    {
        _logger = logger;
        _funcionarioService = funcionarioService;
    }

    [HttpGet]
    public ActionResult<List<FuncionarioDTO>> GetAll()
    {
        try
        {
            List<FuncionarioDTO> res = _funcionarioService.FindAll();
            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar todos os funcionários");
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }

    [HttpGet("{id}")]
    public ActionResult<FuncionarioDTO> GetById(int id)
    {
        try
        {
            FuncionarioDTO? res = _funcionarioService.FindById(id);

            if (res == null)
            {
                return NotFound($"Funcionário com ID {id} não encontrado");
            }

            return Ok(res);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar funcionário por ID: {Id}", id);
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }

    [HttpPost]
    public ActionResult<FuncionarioDTO> Create([FromBody] FuncionarioDTO funcionario)
    {
        try
        {
            if (funcionario == null)
            {
                return BadRequest("Dados do funcionário não podem ser nulos");
            }

            FuncionarioDTO res = _funcionarioService.Create(funcionario);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Erro de validação ao criar funcionário");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar funcionário");
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }

    [HttpPut("{id}")]
    public ActionResult<FuncionarioDTO> Update(int id, [FromBody] FuncionarioDTO funcionario)
    {
        try
        {
            if (funcionario == null)
            {
                return BadRequest("Dados do funcionário não podem ser nulos");
            }

            FuncionarioDTO res = _funcionarioService.Update(id, funcionario);

            if (res == null)
            {
                return NotFound($"Funcionário com ID {id} não encontrado");
            }

            return Ok(res);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "Erro de validação ao atualizar funcionário: {Id}", id);
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar funcionário: {Id}", id);
            return StatusCode(500, "Ocorreu um erro interno ao processar a solicitação");
        }
    }
}
