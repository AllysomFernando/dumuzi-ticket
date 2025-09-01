using DumuziTickets.domain;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;

namespace DumuziTickets.Infra.Persistence.PgSQL.Mappers;

public static class PgTicketMapper
{
    public static PgTicketEntity ToEntity(TicketDTO dto)
    {
            return new PgTicketEntity(
                dto.Quantidade,
                PgFuncionarioMapper.ToEntity(dto.Funcionario),
                (Situacao)dto.Situacao,
                dto.CreatedAt,
                dto.UpdatedAt);
    }

    public static TicketDTO ToDto(PgTicketEntity entity)
    {
        return new TicketDTO
        {
            Id = entity.Id,
            Quantidade = entity.Quantidade,
            Funcionario = PgFuncionarioMapper.ToDto(entity.Funcionario),
            Situacao = (EnumSituacao)entity.Situacao,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
