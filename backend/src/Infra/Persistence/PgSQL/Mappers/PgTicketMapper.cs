using backend.domain;
using backend.domain.entities;
using backend.Infra.Persistence.PgSQL.Entities;
using backend.Infra.Persistence.PgSQL.Enum;

namespace backend.Infra.Persistence.PgSQL.Mappers;

public static class PgTicketMapper
{
    public static PgTicketEntity ToEntity(TicketBO bo)
    {
            return new PgTicketEntity(
                bo.Quantidade,
                bo.Funcionario.Id,
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
