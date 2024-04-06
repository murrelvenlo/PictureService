using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using PictureService.Domain;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.Equipments;

namespace PictureService.Data.Dapper.Repositories.Equipments
{
    public class EquipmentsReader : BaseReader<Equipment>, IReadEquipments, IVerifyEquipments
    {
        #region baseStatements
        private const string BaseSelectStatement = @"
            SELECT 
                [Id]
            ,   [Name]
            ,   [DisplayName]
            ,   [HierarchyType]
            ,   [Comment]
            ,   [IsActive]
            ,   [ParentId]
            ,   [CompanyId]
            ,   [CreatedBy]
            ,   [CreatedAtUtc]
            ,   [ModifiedBy]
            ,   [ModifiedAtUtc]
            ,   [RowVersion]
            FROM [Equipment]
            ";

        private const string BaseExistsStatement = @"
            SELECT
	            [Id]
            FROM [Equipment]
            ";
        #endregion

        private readonly ISql _sql;
        public EquipmentsReader(ISql sql)
        {
            _sql = sql;
        }

        public override async Task<IEnumerable<Equipment>> All()
        {
            const string statement = BaseSelectStatement + ";";
            return await _sql.Connection.QueryAsync<Equipment>(statement, _sql.Transaction);
        }



        public override async Task<Equipment> ById<TId>(TId id)
        {
            const string statement = BaseSelectStatement + "WHERE [Id] = @Id;";
            return await _sql.Connection.QueryFirstOrDefaultAsync<Equipment>(statement, new { id }, _sql.Transaction);
        }

        public override async Task<IEnumerable<Equipment>> MultipleById<TId>(IEnumerable<TId> ids)
        {
            var idList = ids.ToList();

            if (!idList.Any())
                return Enumerable.Empty<Equipment>();

            const string statement = BaseSelectStatement + "WHERE [Id] IN @Ids;";
            return await _sql.Connection.QueryAsync<Equipment>(statement, new { idList }, _sql.Transaction);
        }

        public override async Task<bool> Exists(int id)
        {
            const string statement = BaseExistsStatement + "WHERE [Id] = @Id;";
            return await _sql.Connection.QueryFirstOrDefaultAsync<DataSet>(statement, new { id }, _sql.Transaction) is not null;
        }

        public override Task<Equipment> ByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
