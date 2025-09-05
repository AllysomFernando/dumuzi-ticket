using DumuziTickets.Domain.Assertions;
using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Infra.Persistence.PgSQL.Config;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Enum;
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

    public FuncionarioBO FindByCPF(string cpf)
    {
        PgFuncionarioEntity? entity = _context.Funcionarios.FirstOrDefault(e => e.Cpf == cpf);
        return entity == null ? null : PgFuncionarioMapper.ToBO(entity);
    }

    public FuncionarioBO Create(FuncionarioBO entity)
    {
        PgFuncionarioEntity pgEntity = PgFuncionarioMapper.ToEntity(entity);
        _context.Funcionarios.Add(pgEntity);
        _context.SaveChanges();
        return PgFuncionarioMapper.ToBO(pgEntity);
    }

    public FuncionarioBO Update(int id, FuncionarioBO entity)
    {
        PgFuncionarioEntity pgEntity = _context.Funcionarios.FirstOrDefault(e => e.Id == id);
        Assert.IsNotNull(pgEntity, "Funcionario não encontrado.");
        pgEntity.Id = id;
        pgEntity.Nome = entity.Nome;
        pgEntity.Situacao = (Situacao)entity.Situacao;
        pgEntity.UpdatedAt = entity.UpdatedAt;

        _context.SaveChanges();
        return PgFuncionarioMapper.ToBO(pgEntity);
    }

    public FuncionarioBO Deactivate(int id)
    {
        PgFuncionarioEntity pgEntity = _context.Funcionarios.FirstOrDefault(e => e.Id == id);
        Assert.IsNotNull(pgEntity, "Funcionario não encontrado.");
        pgEntity.Situacao = Situacao.I;
        pgEntity.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
        return PgFuncionarioMapper.ToBO(pgEntity);
    }

    public FuncionarioBO Activate(int id)
    {
        PgFuncionarioEntity pgEntity = _context.Funcionarios.FirstOrDefault(e => e.Id == id);
        Assert.IsNotNull(pgEntity, "Funcionario não encontrado.");
        pgEntity.Situacao = Situacao.A;
        pgEntity.UpdatedAt = DateTime.Now;

        _context.SaveChanges();
        return PgFuncionarioMapper.ToBO(pgEntity);
    }
}
