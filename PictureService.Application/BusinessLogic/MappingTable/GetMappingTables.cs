using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.MappingTable
{
    public class GetMappingTables : IQuery<IEnumerable<GenericMappingTable>>
    {
        public GetMappingTables() : this(Enumerable.Empty<Guid>(), Enumerable.Empty<string>()) { } //Get all tables
        public GetMappingTables(Guid id) : this(new List<Guid> { id }, Enumerable.Empty<string>()) { } //Get table by id
        public GetMappingTables(string tableName) : this(Enumerable.Empty<Guid>(), new List<string> { tableName }) { } //Get table by name
        public GetMappingTables(Guid id, string tableName) : this(new List<Guid> { id }, new List<string> { tableName }) { } //Get table by id and name
        public GetMappingTables(IEnumerable<Guid> ids, IEnumerable<string> tableNames)
        {
            Ids = ids;
            TableNames = tableNames;
        }

        public IEnumerable<Guid> Ids;
        public IEnumerable<string> TableNames;
    }
}
