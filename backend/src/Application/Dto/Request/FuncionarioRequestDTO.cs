using backend.domain;

namespace backend.Application.DTO;

public record FuncionarioRequestDTO()
{
    public int? Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public EnumSituacao Situacao { get; set; }
}

