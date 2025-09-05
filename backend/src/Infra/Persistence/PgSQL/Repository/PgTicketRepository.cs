using backend.Domain.Assertions;
using backend.domain.entities;
using backend.Domain.Repository;
using backend.Infra.Persistence.PgSQL.Config;
using backend.Infra.Persistence.PgSQL.Entities;
using backend.Infra.Persistence.PgSQL.Enum;
using backend.Infra.Persistence.PgSQL.Mappers;
using backend.domain;
using Microsoft.EntityFrameworkCore;

namespace backend.Infra.Persistence.PgSQL.Repository;

public class PgTicketRepository : ITicketRepository
{
        private readonly PostgresDbContext _context;

        public PgTicketRepository(PostgresDbContext context)
        {
                _context = context;
        }
        public IEnumerable<TicketBO> FindAll()
        {
                var entities = _context.Tickets
                        .Include(t => t.Funcionario)
                        .ToList();

                return entities.Select(PgTicketMapper.ToBO);
        }

        public TicketBO FindById(int id)
        {
                var entity = _context.Tickets
                        .Include(t => t.Funcionario)
                        .FirstOrDefault(e => e.Id == id);

                return entity == null ? null : PgTicketMapper.ToBO(entity);
        }

        public IEnumerable<TicketBO> FindByFuncionarioId(int funcionarioId)
        {
                var entities = _context.Tickets
                        .Include(t => t.Funcionario)
                        .Where(t => t.FuncionarioId == funcionarioId)
                        .ToList();

                return entities.Select(PgTicketMapper.ToBO);
        }

        public IEnumerable<TicketBO> FindByFuncionarioRange(int funcionarioId, DateTime dataInicial, DateTime dataFinal)
        {
                var entities = _context.Tickets
                        .Include(t => t.Funcionario)
                        .Where(t =>
                                t.FuncionarioId == funcionarioId &&
                                t.UpdatedAt >= dataInicial &&
                                t.UpdatedAt <= dataFinal
                        )
                        .ToList();

                return entities.Select(PgTicketMapper.ToBO);
        }
        public TicketBO Create(TicketBO entity)
        {
                PgTicketEntity pgEntity = PgTicketMapper.ToEntity(entity);
                pgEntity.UpdatedAt.ToUniversalTime();
                _context.Tickets.Add(pgEntity);
                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }

        public TicketBO Update(int id, TicketBO entity)
        {
                PgTicketEntity pgEntity = _context.Tickets.FirstOrDefault(e => e.Id == id);
                Assert.IsNotNull(pgEntity, "Ticket não encontrado.");
                pgEntity.Id = entity.Id;
                pgEntity.FuncionarioId = entity.Funcionario.Id;
                pgEntity.Situacao = (Situacao)entity.Situacao;
                pgEntity.Quantidade = entity.Quantidade;

                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }

        public TicketBO Activate(int id)
        {
                PgTicketEntity pgEntity = _context.Tickets.FirstOrDefault(e => e.Id == id);
                Assert.IsNotNull(pgEntity, "Ticket não encontrado.");
                pgEntity.Situacao = Situacao.A;
                pgEntity.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }
        public TicketBO Deactivate(int id)
        {
                PgTicketEntity pgEntity = _context.Tickets.FirstOrDefault(e => e.Id == id);
                Assert.IsNotNull(pgEntity, "Ticket não encontrado.");
                pgEntity.Situacao = Situacao.I;
                pgEntity.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }
}
