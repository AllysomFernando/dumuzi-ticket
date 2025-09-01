using DumuziTickets.domain.entities;

namespace DumuziTickets.Domain.Repository;

public interface IRepository<TEntity, TKey>
{
    IEnumerable<TEntity> FindAll();
    FuncionarioBO? FindById(TKey id);
    TEntity Create(TEntity entity);
    TEntity Update(TEntity entity);
}
