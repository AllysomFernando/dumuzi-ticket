using DumuziTickets.domain;

namespace DumuziTickets.Domain.Dto;

public record FuncionarioDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public Situacao situacao { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}