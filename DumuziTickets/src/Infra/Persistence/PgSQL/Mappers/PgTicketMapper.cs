using DumuziTickets.domain;
using DumuziTickets.domain.entities;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;

namespace DumuziTickets.Infra.Persistence.PgSQL.Mappers;

public static class PgTicketMapper
{
    public static PgTicketEntity ToEntity(TicketBO bo)
    {
            return new PgTicketEntity(
                bo.Quantidade,
                PgFuncionarioMapper.ToEntity(bo.Funcionario),
                (Situacao)bo.Situacao,
                bo.UpdatedAt);
    }

    public static TicketBO ToBO(PgTicketEntity entity)
    {
        return new TicketBO(
            entity.Id,
            entity.Quantidade,
            PgFuncionarioMapper.ToBO(entity.Funcionario),
            (EnumSituacao)entity.Situacao,
            entity.UpdatedAt
        );

    }
}
