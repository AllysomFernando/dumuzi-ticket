using backend.domain;
using backend.domain.entities;
using backend.Infra.Persistence.PgSQL.Entities;
using backend.Infra.Persistence.PgSQL.Enum;
using backend.Domain.Dto;

namespace backend.Infra.Persistence.PgSQL.Mappers;

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
