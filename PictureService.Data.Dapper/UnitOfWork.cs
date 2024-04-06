using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using PictureService.Domain;

namespace PictureService.Data.Dapper;

#region Dapper Type Handlers

public class NullableDateTimeHandler : SqlMapper.TypeHandler<DateTime?>
{
    public override void SetValue(IDbDataParameter parameter, DateTime? value)
    {
        parameter.Value = value;
    }

    public override DateTime? Parse(object value)
    {
        return DateTime.TryParse(value.ToString(), out var datetime)
            ? (DateTime?)DateTime.SpecifyKind(datetime, DateTimeKind.Local)
            : null;
    }
}

public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
{
    public override void SetValue(IDbDataParameter parameter, DateTime value)
    {
        parameter.Value = value;
    }

    public override DateTime Parse(object value)
    {
        return DateTime.SpecifyKind((DateTime)value, DateTimeKind.Local);
    }
}

#endregion

public class UnitOfWork : ISql, IUnitOfWork
{
    private readonly IConnectionString _connectionString;
    private IDbConnection? _connection = null;

    public IDbTransaction? Transaction { get; private set; } = null;

    public UnitOfWork(IConnectionString connectionString)
    {
        DefaultTypeMap.MatchNamesWithUnderscores = true;
        this._connectionString = connectionString;
        SqlMapper.AddTypeHandler(new DateTimeHandler());
        SqlMapper.AddTypeHandler(new NullableDateTimeHandler());
    }

    public IDbConnection Connection
    {
        get
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                return _connection;
            }

            return new SqlConnection(_connectionString.Value);
        }
    }

    public void Open()
    {
        _connection = new SqlConnection(_connectionString.Value);
        _connection.Open();
    }

    public void Begin()
    {
        Transaction = _connection?.BeginTransaction();
    }

    public void Commit()
    {
        Transaction?.Commit();
        Dispose();
    }

    public void Dispose()
    {
        _connection?.Close();
        Transaction?.Dispose();

        _connection = null;
        Transaction = null;
    }

    public void Rollback()
    {
        Transaction?.Rollback();
        Dispose();
    }
}