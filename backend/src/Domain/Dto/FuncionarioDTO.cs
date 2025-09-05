using DumuziTickets.domain;

namespace DumuziTickets.Domain.Dto;

public record FuncionarioDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public EnumSituacao Situacao { get; set; }
    public DateTime UpdatedAt { get; set; }
}
