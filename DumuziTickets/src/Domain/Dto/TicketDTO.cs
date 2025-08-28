using DumuziTickets.domain;

namespace DumuziTickets.Domain.Dto;

public record TicketDTO
{
    public int Id { get; set; }
    public FuncionarioDTO Funcionario { get; set; }
    public int Quantidade { get; set; }
    public Situacao  Situacao { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}