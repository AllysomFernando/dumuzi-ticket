using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DumuziTickets.Infra.Persistence.PgSQL.Config;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
    }

    public DbSet<PgFuncionarioEntity> Funcionarios { get; set; }
    public DbSet<PgTicketEntity> Tickets { get; set; }

}
