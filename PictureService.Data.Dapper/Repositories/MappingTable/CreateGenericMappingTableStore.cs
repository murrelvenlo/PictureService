using Dapper;
using PictureService.Domain;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.MappingTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Data.Dapper.Repositories.MappingTable
{
    public class CreateGenericMappingTableStore : BaseStore<GenericMappingTable>, ICreateGenericMappingTable
    {
        #region baseStatements

        private const string BaseInsertStatement = @"
        INSERT INTO [GenericMappingTable]
        (   [TableName]
        ,	[EntityId]
        ,	[KeyValuePairs]
        ,	[CreatedBy]
        ,	[CreatedAtUtc]
        ,	[ModifiedBy]
        ,	[ModifiedAtUtc]
        ,	[RowVersion]
        )
        OUTPUT Inserted.*
        VALUES
        (   @TableName
        ,   @EntityId
        ,   @KeyValuePairs
        ,   @CreatedBy
        ,   @CreatedAtUtc
        ,   @CreatedBy
        ,   @CreatedAtUtc
        ,   @RowVersion
        );";
        #endregion
        private const string SystemUserName = "PictureServiceAPI";
        private readonly ISql _sql;

        public CreateGenericMappingTableStore(ISql sql)
        {
            _sql = sql;
        }

        public override async Task<Guid> Add(GenericMappingTable entity)
        {
            //var mappingId = Guid.NewGuid();

            return await _sql.Connection.QuerySingleAsync<Guid>(BaseInsertStatement,
                new
                {
                    entity.TableName
                ,
                    entity.EntityId
                ,
                    entity.KeyValuePairs
                ,
                    CreatedBy = SystemUserName
                ,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                ,
                    RowVersion = 1
                },
                _sql.Transaction);
        }

        public override Task Delete(GenericMappingTable entity)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public override Task DeleteMultipleById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<GenericMappingTable> Store(GenericMappingTable entity)
        {
            return await _sql.Connection.QuerySingleAsync<GenericMappingTable>(BaseInsertStatement,
                new
                {
                    entity.TableName
                ,
                    entity.EntityId
                ,
                    entity.KeyValuePairs
                ,
                    CreatedBy = SystemUserName
                ,
                    CreatedAtUtc = DateTimeOffset.UtcNow
                ,
                    RowVersion = 1
                },
                _sql.Transaction);
        }

        public override Task Update(GenericMappingTable entity)
        {
            throw new NotImplementedException();
        }
    }
}
