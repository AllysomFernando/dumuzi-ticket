using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Infra.Persistence.PgSQL.Config;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Mappers;
using Microsoft.EntityFrameworkCore;

namespace DumuziTickets.Infra.Persistence.PgSQL.Repository;

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

        public TicketBO? FindByFuncionarioId(int funcionarioId)
        {
                var entity = _context.Tickets
                        .Include(t => t.Funcionario)
                        .FirstOrDefault(e => e.FuncionarioId == funcionarioId);

                return entity == null ? null : PgTicketMapper.ToBO(entity);
        }

        public TicketBO Create(TicketBO entity)
        {
                PgTicketEntity pgEntity = PgTicketMapper.ToEntity(entity);
                _context.Tickets.Add(pgEntity);
                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }

        public TicketBO Update(int id, TicketBO entity)
        {
                PgTicketEntity pgEntity = PgTicketMapper.ToEntity(entity);
                _context.Tickets.Update(pgEntity);
                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }
}
