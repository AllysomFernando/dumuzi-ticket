using backend.domain;

namespace backend.Domain.Dto;

public record TicketDTO
{
    public int Id { get; set; }
    public FuncionarioDTO Funcionario { get; set; }
    public int Quantidade { get; set; }
    public EnumSituacao  Situacao { get; set; }
    public DateTime UpdatedAt { get; set; }
}
