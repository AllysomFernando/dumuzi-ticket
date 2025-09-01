using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

public class PgTicketEntity
{
    [Key] public int Id { get; private set; }
    [Required] public int FuncionarioId { get; private set; }
    [ForeignKey(nameof(FuncionarioId))] public PgFuncionarioEntity Funcionario { get; private set; }
    [Required] public int Quantidade { get; private set; }
    [Required] public PgSituacaoEntity Situacao { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime UpdatedAt { get; private set; }

    public PgTicketEntity(int quantidade, PgFuncionarioEntity funcionario, PgSituacaoEntity situacao)
    {
        Quantidade = quantidade;
        Funcionario = funcionario;
        Situacao = situacao;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
