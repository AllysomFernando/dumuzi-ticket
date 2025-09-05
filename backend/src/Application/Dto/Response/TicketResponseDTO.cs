using backend.Application.DTO;
using backend.domain;

namespace backend.Application.Dto.Response;

public record TicketResponseDTO()
{
    public int? Id { get; set; }
    public FuncionarioRequestDTO Funcionario { get; set; }
    public int Quantidade { get; set; }
    public EnumSituacao Situacao { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
