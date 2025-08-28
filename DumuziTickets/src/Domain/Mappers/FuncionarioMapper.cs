using DumuziTickets.Domain.Dto;
using DumuziTickets.domain.entities;

namespace DumuziTickets.Domain.Mappers;

public class FuncionarioMapper
{
    public static FuncionarioDTO ToDTO(FuncionarioBO bo)
    {
        FuncionarioDTO dto = new FuncionarioDTO();
        dto.Id = bo.Id;
        dto.Nome = bo.Nome;
        dto.Cpf = bo.Cpf;
        dto.Situacao = bo.Situacao;
        dto.CreatedAt = bo.CreatedAt;
        dto.UpdatedAt = bo.UpdatedAt;
        return dto;
    }

    public static FuncionarioBO ToBO(FuncionarioDTO dto)
    {
        return new FuncionarioBO(dto.Id, dto.Nome, dto.Cpf, dto.Situacao, dto.CreatedAt, dto.UpdatedAt);
    }
}