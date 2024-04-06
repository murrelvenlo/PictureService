namespace PictureService.Domain.Repositories;

public interface IBaseCreator<T> where T : class, new()
{
    Task<Guid> Add(T entity);
}