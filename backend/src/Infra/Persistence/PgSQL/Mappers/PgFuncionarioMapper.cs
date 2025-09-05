using DumuziTickets.domain;
using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;

namespace DumuziTickets.Infra.Persistence.PgSQL.Mappers;

public static class PgFuncionarioMapper
{
    public static PgFuncionarioEntity ToEntity(FuncionarioBO bo)
    {
        return new PgFuncionarioEntity(bo.Nome, bo.Cpf, (Situacao)bo.Situacao, bo.UpdatedAt);
    }

    public static FuncionarioBO ToBO(PgFuncionarioEntity entity)
    {
        return new FuncionarioBO(
                entity.Id,
                entity.Nome,
                entity.Cpf,
                (EnumSituacao)entity.Situacao,
                entity.UpdatedAt
            );
    }
}
