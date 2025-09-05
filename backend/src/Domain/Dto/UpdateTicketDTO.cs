using DumuziTickets.domain;

namespace DumuziTickets.Domain.Dto;

public record UpdateTicketDTO()
{
    public int Quantidade { get; set; }
    public int FuncionarioId { get; set; }
    public EnumSituacao Situacao { get; set; }
}
