using PictureService.Domain.Repositories;

namespace PictureService.Data.Dapper.Repositories;

public abstract class BaseReader<T> : IBaseReader<T>, IBaseVerifier<T> where T : class, new()
{
    public abstract Task<IEnumerable<T>> All();
    public abstract Task<T> ById<TId>(TId id);
    public abstract Task<IEnumerable<T>> MultipleById<TId>(IEnumerable<TId> ids);
    public abstract Task<bool> Exists(int id);

    //Murrel Venlo
    public abstract Task<T> ByName(string name);
}