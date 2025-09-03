using DumuziTickets.domain.entities;

namespace DumuziTickets.Domain.Repository;

public interface IRepository<TEntity, TKey>
{
    IEnumerable<TEntity> FindAll();
    TEntity? FindById(TKey id);
    TEntity Create(TEntity entity);
    TEntity Update(TKey id, TEntity entity);
}
