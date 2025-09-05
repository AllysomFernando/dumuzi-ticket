using backend.domain;
using backend.Application.DTO;

namespace backend.Application.Dto.Response;

public record FuncionarioResponseDTO()
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public EnumSituacao Situacao { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
