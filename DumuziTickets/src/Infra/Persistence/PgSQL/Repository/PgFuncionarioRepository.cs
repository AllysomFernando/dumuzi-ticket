using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Infra.Persistence.PgSQL.Config;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Mappers;

namespace DumuziTickets.Infra.Persistence.PgSQL.Repository;

public class PgFuncionarioRepository : IFuncionarioRepository
{
    private readonly PostgresDbContext _context;

    public PgFuncionarioRepository(PostgresDbContext context)
    {
        _context = context;
    }

    public IEnumerable<FuncionarioBO> FindAll()
    {
        IEnumerable<PgFuncionarioEntity> entities = _context.Funcionarios.ToList();
        return entities.Select(PgFuncionarioMapper.ToBO);
    }

    public FuncionarioBO? FindById(int id)
    {
        PgFuncionarioEntity? entity = _context.Funcionarios.FirstOrDefault(e => e.Id == id);
        return entity == null ? null : PgFuncionarioMapper.ToBO(entity);
    }

    public FuncionarioBO Create(FuncionarioBO entity)
    {
        PgFuncionarioEntity pgEntity = PgFuncionarioMapper.ToEntity(entity);
        _context.Funcionarios.Add(pgEntity);
        _context.SaveChanges();
        return PgFuncionarioMapper.ToBO(pgEntity);
    }

    public FuncionarioBO Update(FuncionarioBO entity)
    {
        PgFuncionarioEntity pgEntity = PgFuncionarioMapper.ToEntity(entity);
        _context.Funcionarios.Update(pgEntity);
        _context.SaveChanges();
        return PgFuncionarioMapper.ToBO(pgEntity);
    }
}
