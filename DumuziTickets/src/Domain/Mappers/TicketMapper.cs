using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;

namespace DumuziTickets.Domain.Mappers;

public class TicketMapper
{
    public static TicketDTO ToDto(TicketBO bo)
    {
        TicketDTO dto = new TicketDTO();

        dto.Id = bo.Id;
        dto.Funcionario = FuncionarioMapper.ToDTO(bo.Funcionario);
        dto.Quantidade = bo.Quantidade;
        dto.Situacao = bo.Situacao;
        dto.CreatedAt = bo.CreatedAt;
        dto.UpdatedAt = bo.UpdatedAt;
        return dto;
    }

    public static TicketBO ToBo(TicketDTO dto)
    {
        FuncionarioBO funcionario = FuncionarioMapper.ToBO(dto.Funcionario);
        return new TicketBO(dto.Id, dto.Quantidade, funcionario, dto.Situacao, dto.CreatedAt, dto.UpdatedAt);
    }
}
