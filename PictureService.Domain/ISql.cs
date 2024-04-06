using System.Data;

namespace PictureService.Domain;

public interface ISql
{
    IDbConnection? Connection { get; }

    IDbTransaction? Transaction { get; }
}