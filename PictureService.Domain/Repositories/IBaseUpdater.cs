namespace PictureService.Domain.Repositories;

public interface IBaseUpdater<T> where T: class, new()
{
    Task Update(T entity);
}