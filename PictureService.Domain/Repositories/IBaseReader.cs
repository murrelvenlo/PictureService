namespace PictureService.Domain.Repositories;

public interface IBaseReader<T> where T : class, new()
{
    public Task<IEnumerable<T>> All();
    //public Task<T> ById(int id);
    public Task<T> ById<TId>(TId id);
    //public Task<IEnumerable<T>> MultipleById(IEnumerable<int> ids);
    Task<IEnumerable<T>> MultipleById<TId>(IEnumerable<TId> ids);
    //Murrel Venlo
    public Task<T> ByName(string name);
}