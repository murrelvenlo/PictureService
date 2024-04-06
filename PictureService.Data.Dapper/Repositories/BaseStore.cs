using PictureService.Domain.Repositories;

namespace PictureService.Data.Dapper.Repositories;

public abstract class BaseStore<T> : IBaseCreator<T>, IBaseUpdater<T>, IBaseDeleter<T> where T : class, new()
{
    public abstract Task<Guid> Add(T entity);
    public abstract Task Update(T entity);
    public abstract Task DeleteMultipleById(IEnumerable<int> ids);
    public abstract Task DeleteById(int id);
    public abstract Task Delete(T entity);
}