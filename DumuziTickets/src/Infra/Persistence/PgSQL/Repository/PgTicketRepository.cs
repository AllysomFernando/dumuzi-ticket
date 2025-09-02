using DumuziTickets.domain.entities;
using DumuziTickets.Domain.Repository;
using DumuziTickets.Infra.Persistence.PgSQL.Config;
using DumuziTickets.Infra.Persistence.PgSQL.Entities;
using DumuziTickets.Infra.Persistence.PgSQL.Mappers;

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
                IEnumerable<PgTicketEntity> entities = _context.Tickets.ToList();
                return entities.Select(PgTicketMapper.ToBO);
        }

        public TicketBO FindById(int id)
        {
                PgTicketEntity? entity = _context.Tickets.FirstOrDefault(e => e.Id == id);
                return entity == null ? null : PgTicketMapper.ToBO(entity);
        }

        public TicketBO Create(TicketBO entity)
        {
                PgTicketEntity pgEntity = PgTicketMapper.ToEntity(entity);
                _context.Tickets.Add(pgEntity);
                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }

        public TicketBO Update(TicketBO entity)
        {
                PgTicketEntity pgEntity = PgTicketMapper.ToEntity(entity);
                _context.Tickets.Update(pgEntity);
                _context.SaveChanges();
                return PgTicketMapper.ToBO(pgEntity);
        }
}
