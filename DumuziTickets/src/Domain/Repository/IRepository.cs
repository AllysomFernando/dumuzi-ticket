namespace DumuziTickets.Domain.Repository;

public interface IRepository<TEntity, TKey>
{
    IEnumerable<TEntity> FindAll();
    TEntity FindById(TKey id);
    TEntity Create(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TKey id);
}
