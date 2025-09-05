using backend.domain.entities;

namespace backend.Domain.Repository;

public interface IRepository<TEntity, TKey>
{
    IEnumerable<TEntity> FindAll();
    TEntity? FindById(TKey id);
    TEntity Create(TEntity entity);
    TEntity Update(TKey id, TEntity entity);
    TEntity Activate(TKey id);
    TEntity Deactivate(TKey id);
}
