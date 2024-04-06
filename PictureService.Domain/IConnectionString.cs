namespace PictureService.Domain;

public interface IConnectionString
{
    string Value { get; }
}

public class ConnectionString : IConnectionString
{
    public ConnectionString(string connectionString)
    {
        Value = connectionString;
    }

    public string Value { get; }
}