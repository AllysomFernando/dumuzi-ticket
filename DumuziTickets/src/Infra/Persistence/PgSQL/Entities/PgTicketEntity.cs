using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

public class PgTicketEntity
{
    [Key] public int Id { get; private set; }
    [Required] public int FuncionarioId { get; private set; }
    [ForeignKey(nameof(FuncionarioId))] public PgFuncionarioEntity Funcionario { get; private set; }
    [Required] public int Quantidade { get; private set; }
    [Required] public Situacao Situacao { get; private set; }
    [Required] public DateTime UpdatedAt { get; private set; }

    public PgTicketEntity(int quantidade, int funcionarioId, Situacao situacao, DateTime updatedAt)
    {
        Quantidade = quantidade;
        FuncionarioId = funcionarioId;
        Situacao = situacao;
        UpdatedAt = updatedAt;
    }
    public PgTicketEntity()
    {
    }
}
