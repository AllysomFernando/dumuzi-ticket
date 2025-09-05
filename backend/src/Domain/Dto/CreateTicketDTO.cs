namespace backend.Domain.Dto;

public record CreateTicketDTO
{
    public int Quantidade { get; set; }
    public int FuncionarioId { get; set; }
}
