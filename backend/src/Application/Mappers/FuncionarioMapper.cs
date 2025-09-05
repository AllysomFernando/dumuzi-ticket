using DumuziTickets.Application.DTO;
using DumuziTickets.Application.Dto.Response;
using DumuziTickets.domain.entities;

namespace DumuziTickets.Application.Mappers;

public class FuncionarioMapper
{
    public static FuncionarioBO ToDomain(FuncionarioRequestDTO dto)
    {
        return new FuncionarioBO(
            dto.Id ?? 0,
            dto.Nome,
            dto.Cpf,
            dto.Situacao,
            dto.CreatedAt ?? DateTime.UtcNow,
            dto.UpdatedAt ?? DateTime.UtcNow
        );
    }

    public static FuncionarioResponseDTO ToResponse(FuncionarioBO bo)
    {
        return new FuncionarioResponseDTO
        {
            Id = bo.Id,
            Nome = bo.Nome,
            Cpf = bo.Cpf,
            Situacao = bo.Situacao,
            CreatedAt = bo.CreatedAt,
            UpdatedAt = bo.UpdatedAt
        };
    }
}
