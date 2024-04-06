namespace PictureService.Domain.Repositories;

public interface IBaseDeleter<T> where T : class, new()
{
    Task DeleteMultipleById(IEnumerable<int> ids);

    Task DeleteById(int id);

    Task Delete(T entity);
}