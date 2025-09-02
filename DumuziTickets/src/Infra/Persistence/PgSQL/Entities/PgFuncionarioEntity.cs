using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;
using Microsoft.EntityFrameworkCore;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

[Table("funcionario")]
[Index(nameof(Cpf), IsUnique = true)]
public class PgFuncionarioEntity
{
    [Key] public int Id { get; private set; }
    [Required] public string Nome { get; private set; }
    [Required] public string Cpf { get; private set; }
    [Required] public Situacao Situacao { get; private set; }
    [Required] public DateTime UpdatedAt { get; private set; }

    public PgFuncionarioEntity(string nome, string cpf, Situacao situacao, DateTime updatedAt)
    {
        Nome = nome;
        Cpf = cpf;
        Situacao = situacao;
        UpdatedAt = updatedAt;
    }

    public PgFuncionarioEntity() { }
}
