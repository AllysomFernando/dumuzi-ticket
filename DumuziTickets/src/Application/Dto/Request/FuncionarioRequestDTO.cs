using DumuziTickets.domain;

namespace DumuziTickets.Application.DTO;

public record FuncionarioRequestDTO()
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public EnumSituacao Situacao { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

