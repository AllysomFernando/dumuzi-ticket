using System.ComponentModel.DataAnnotations;

namespace DumuziTickets.Infra.Persistence.PgSQL.Entities;

public class PgSituacaoEntity
{
    [Key] public int Id { get; private set; }
    [Required] public string Nome { get; private set; }

    public PgSituacaoEntity(string nome)
    {
        Nome = nome;
    }
}
