using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.MappingTable
{
    public class AddMappingTable : ICommand<GenericMappingTable>
    {
        public AddMappingTable(string? tableName, int? entityId, string? keyValuePairs)
        {
            TableName = tableName;
            EntityId = entityId;
            KeyValuePairs = keyValuePairs;
        }

        public string? TableName { get; set; }
        public int? EntityId { get; set; }
        public string? KeyValuePairs { get; set; }
    }
}
