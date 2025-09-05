using backend.Domain.Dto;
using backend.domain.entities;

namespace backend.Domain.Mappers;

public class TicketMapper
{
    public static TicketDTO ToDTO(TicketBO bo)
    {
        TicketDTO dto = new TicketDTO();

        dto.Id = bo.Id;
        dto.Funcionario = FuncionarioMapper.ToDTO(bo.Funcionario);
        dto.Quantidade = bo.Quantidade;
        dto.Situacao = bo.Situacao;
        dto.UpdatedAt = bo.UpdatedAt;
        return dto;
    }

    public static TicketBO ToBO(TicketDTO dto)
    {
        FuncionarioBO funcionario = FuncionarioMapper.ToBO(dto.Funcionario);
        return new TicketBO(dto.Id, dto.Quantidade, funcionario, dto.Situacao,  dto.UpdatedAt);
    }
}
