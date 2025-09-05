using backend.Application.Service.Interfaces;
using backend.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace backend.Presentation.Controllers;
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

            List<FuncionarioDTO> res = _funcionarioService.FindAll();
            return Ok(res);

    }

    [HttpGet("{id}")]
    public ActionResult<FuncionarioDTO> GetById(int id)
    {

            FuncionarioDTO? res = _funcionarioService.FindById(id);

            if (res == null)
            {
                return NotFound($"Funcionário com ID {id} não encontrado");
            }

            return Ok(res);

    }

    [HttpPost]
    public ActionResult<FuncionarioDTO> Create([FromBody] FuncionarioDTO funcionario)
    {

            if (funcionario == null)
            {
                return BadRequest("Dados do funcionário não podem ser nulos");
            }

            FuncionarioDTO res = _funcionarioService.Create(funcionario);
            return CreatedAtAction(nameof(GetById), new { id = res.Id }, res);

    }

    [HttpPut("{id}")]
    public ActionResult<FuncionarioDTO> Update(int id, [FromBody] FuncionarioDTO funcionario)
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

    [HttpPatch("deactivate/{id}")]
    public ActionResult<FuncionarioDTO> Deactivate(int id)
    {
        if (id == null)
        {
            return BadRequest("O ID do funcionário não pode ser nulo");
        }
        FuncionarioDTO res = _funcionarioService.Deactivate(id);

        if (res == null)
        {
            return NotFound($"Funcionário com ID {id} não encontrado");
        }

        return Ok(res);
    }

    [HttpPatch("activate/{id}")]
    public ActionResult<FuncionarioDTO> Activate(int id)
    {
        if (id == null)
        {
            return BadRequest("O ID do funcionário não pode ser nulo");
        }

        FuncionarioDTO res = _funcionarioService.Activate(id);
        if (res == null)
        {
            return NotFound($"Funcionário com ID {id} não encontrado");
        }

        return Ok(res);
    }
}
