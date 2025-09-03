using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;
using Microsoft.EntityFrameworkCore;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

[Table("funcionario")]
[Index(nameof(Cpf), IsUnique = true)]
public class PgFuncionarioEntity
{
    [Key] public int Id { get; set; }
    [Required] public string Nome { get; set; }
    [Required] public string Cpf { get; set; }
    [Required] public Situacao Situacao { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }

    public PgFuncionarioEntity(string nome, string cpf, Situacao situacao, DateTime updatedAt)
    {
        Nome = nome;
        Cpf = cpf;
        Situacao = situacao;
        UpdatedAt = updatedAt;
    }

    public PgFuncionarioEntity() { }
}
