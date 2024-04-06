using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.MappingTable
{
    public class GetEntity : IQuery<IEnumerable<GenericMappingTable>>
    {

        public GetEntity(int entityId, string tableName)
        {
            EntityId = entityId;
            TableName = tableName;
        }

        public int EntityId;
        public string TableName;
    }
}
