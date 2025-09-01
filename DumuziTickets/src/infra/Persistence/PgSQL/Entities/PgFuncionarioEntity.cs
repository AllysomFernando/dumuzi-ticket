using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

[Table("funcionario")]
[Index(nameof(Cpf), IsUnique = true)]
public class PgFuncionarioEntity
{
    [Key] public int Id { get; private set; }
    [Required] public string Nome { get; private set; }
    [Required] public string Cpf { get; private set; }
    [Required] public int SituacaoId { get; private set; }
    [ForeignKey(nameof(SituacaoId))] public PgSituacaoEntity Situacao { get; private set; }
    [Required] public DateTime CreatedAt { get; private set; }
    [Required] public DateTime UpdatedAt { get; private set; }

    public PgFuncionarioEntity(string nome, string cpf, PgSituacaoEntity situacao)
    {
        Nome = nome;
        Cpf = cpf;
        Situacao = situacao;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }
}
