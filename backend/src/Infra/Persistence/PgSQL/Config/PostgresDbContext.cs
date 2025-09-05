using backend.Infra.Persistence.PgSQL.Entities;
using backend.Infra.Persistence.PgSQL.Enum;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Persistence.PgSQL.Config;

public class PostgresDbContext : DbContext
{
    public PostgresDbContext(DbContextOptions<PostgresDbContext> options) : base(options)
    {
    }

    public DbSet<PgFuncionarioEntity> Funcionarios { get; set; }
    public DbSet<PgTicketEntity> Tickets { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties()
                         .Where(p => p.ClrType == typeof(Situacao)))
            {
                property.SetMaxLength(1);
                property.SetValueConverter(new Microsoft.EntityFrameworkCore.Storage.ValueConversion.EnumToStringConverter<Situacao>());
            }
        }

        base.OnModelCreating(modelBuilder);
    }

}
