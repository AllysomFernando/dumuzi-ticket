using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

[Table("ticket")]
public class PgTicketEntity
{
    [Key] public int Id { get; set; }
    [Required] public int FuncionarioId { get; set; }
    [ForeignKey(nameof(FuncionarioId))] public PgFuncionarioEntity Funcionario { get;  set; }
    [Required] public int Quantidade { get; set; }
    [Required][Column(TypeName = "varchar(1)")] public Situacao Situacao { get;  set; }
    [Required][Column(TypeName = "timestamp")] public DateTime UpdatedAt { get; set; }

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
