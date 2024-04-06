using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Domain.Repositories.MappingTable
{
    public interface IReadGenericMappingTables : IBaseReader<GenericMappingTable>
    {
        Task<bool> Exist(string name);

        // I need this for the insertion
        Task<T> ByCriteria<T>(Expression<Func<T, bool>> predicate);
        Task<GenericMappingTable> ByEntityId(int id);
        Task<GenericMappingTable> ByEntityIdAndTableName(int id, string tableName);
        Task<GenericMappingTable> ByMappingIdAndEntityId(int id, Guid mappingId);
    }
}
