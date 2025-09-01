using DumuziTickets.domain;
using DumuziTickets.Domain.Dto;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;

namespace DumuziTickets.Infra.Persistence.PgSQL.Mappers;

public static class PgFuncionarioMapper
{
    public static PgFuncionarioEntity ToEntity(FuncionarioDTO dto)
    {
        return new PgFuncionarioEntity(dto.Nome, dto.Cpf, (Situacao)dto.Situacao, dto.CreatedAt, dto.UpdatedAt);
    }

    public static FuncionarioDTO ToDto(PgFuncionarioEntity entity)
    {
        return new FuncionarioDTO
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Cpf = entity.Cpf,
            Situacao = (EnumSituacao)entity.Situacao,
            CreatedAt = entity.CreatedAt,
            UpdatedAt = entity.UpdatedAt
        };
    }
}
