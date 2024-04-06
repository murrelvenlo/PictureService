using PictureService.Domain.Models;
using PictureService.Domain.Repositories.MappingTable;
using PictureService.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PictureService.Data.Dapper.Repositories.MappingTable
{
    public class GenericMappingTablesReader : BaseReader<GenericMappingTable>, IReadGenericMappingTables
    {
        private readonly ISql _sql;
        public GenericMappingTablesReader(ISql sql)
        {
            _sql = sql;
        }

        #region baseStatements
        private const string BaseSelectStatement = @"
        SELECT 
	        [MappingId]
        ,	[TableName]
        ,	[EntityId]
        ,	[KeyValuePairs]
        ,	[CreatedBy]
        ,	[CreatedAtUtc]
        ,	[ModifiedBy]
        ,	[ModifiedAtUtc]
        ,	[RowVersion]
        FROM [GenericMappingTable]
        ";

        private const string BaseExistsStatement = @"
        SELECT
	        [MappingId]
        FROM [GenericMappingTable]
        ";
        #endregion

        public override async Task<IEnumerable<GenericMappingTable>> All()
        {
            const string statement = BaseSelectStatement + ";";
            return await _sql.Connection.QueryAsync<GenericMappingTable>(statement, _sql.Transaction);
        }

        public override Task<bool> Exists(int id)
        {
            throw new NotImplementedException();
        }

        private async Task<GenericMappingTable?> GetTableByName(string tableName)
        {
            const string statement = BaseSelectStatement + "WHERE TableName = @TableName";
            return await _sql.Connection.QueryFirstOrDefaultAsync<GenericMappingTable?>(statement, new { TableName = tableName });
        }

        // Get the tables that are already mapped in the GenericMappingTable
        public override async Task<GenericMappingTable> ByName(string name)
        {
            const string statement = BaseSelectStatement + "WHERE TableName = @TableName";
            return await _sql.Connection.QueryFirstOrDefaultAsync<GenericMappingTable?>(statement, new { TableName = name });
        }

        public override async Task<GenericMappingTable> ById<Guid>(Guid id)
        {
            const string statement = BaseSelectStatement + "WHERE [MappingId] = @MappingId;";
            return await _sql.Connection.QuerySingleOrDefaultAsync<GenericMappingTable>(statement, new { MappingId = id }, _sql.Transaction);

        }

        public override async Task<IEnumerable<GenericMappingTable>> MultipleById<Guid>(IEnumerable<Guid> ids)
        {
            var idList = ids.ToList();

            if (!idList.Any())
                return Enumerable.Empty<GenericMappingTable>();

            const string statement = BaseSelectStatement + "WHERE [MappingId] = @MappingId;";
            return await _sql.Connection.QueryAsync<GenericMappingTable>(statement, new { idList }, _sql.Transaction);
        }


        public async Task<bool> Exist(string name)
        {
            const string statement = "SELECT COUNT(*) FROM sys.tables WHERE [name] = @TableName";

            var result = await _sql.Connection.ExecuteScalarAsync<int>(statement, new { TableName = name });

            //If greather than 0, then table exists
            return result > 0;
        }

        public async Task<T> ByCriteria<T>(Expression<Func<T, bool>> predicate)
        {
            // Construct SQL query dynamically based on the predicate
            var query = new StringBuilder("SELECT * FROM ");
            query.Append(typeof(T).Name); // Assuming table name is the same as entity name
            query.Append(" WHERE ");
            query.Append(predicate.Body);

            // Execute the query
            return await _sql.Connection.QueryFirstOrDefaultAsync<T>(query.ToString());
        }

        public async Task<GenericMappingTable> ByEntityId(int id)
        {
            const string statement = BaseSelectStatement + "WHERE EntityId = @EntityId";
            return await _sql.Connection.QueryFirstOrDefaultAsync<GenericMappingTable?>(statement, new { EntityId = id });
        }

        public async Task<GenericMappingTable> ByEntityIdAndTableName(int id, string tableName)
        {
            const string statement = BaseSelectStatement + "WHERE EntityId = @EntityId AND TableName = @TableName";
            return await _sql.Connection.QueryFirstOrDefaultAsync<GenericMappingTable?>(statement, new { EntityId = id, TableName = tableName });
        }

        public async Task<GenericMappingTable> ByMappingIdAndEntityId(int id, Guid mappingId)
        {
            const string statement = BaseSelectStatement + "WHERE EntityId = @EntityId AND MappingId = @MappingId";
            return await _sql.Connection.QueryFirstOrDefaultAsync<GenericMappingTable?>(statement, new { EntityId = id, MappingId = mappingId });
        }
    }
}
