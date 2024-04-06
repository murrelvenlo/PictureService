namespace PictureService.Domain.Repositories;

public interface IBaseVerifier<T> where T : class, new()
{
    public Task<bool> Exists(int id);
}