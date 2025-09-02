using DumuziTickets.domain;

namespace DumuziTickets.Application.DTO;

public record TicketRequestDTO()
{
    public int? Id { get; set; }
    public FuncionarioRequestDTO Funcionario { get; set; }
    public int Quantidade { get; set; }
    public EnumSituacao Situacao { get; set; }
}
