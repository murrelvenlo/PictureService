namespace PictureService.Domain;

public interface IUnitOfWork : IDisposable
{
    void Open();

    void Begin();

    void Commit();

    void Rollback();
}