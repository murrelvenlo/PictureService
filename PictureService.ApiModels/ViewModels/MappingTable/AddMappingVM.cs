using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.ViewModels.MappingTable
{
    public class AddMappingVM
    {
        public string? TableName { get; set; }

        //public string? KeyValuePairs { get; set; }
        public int? EntityId { get; set; }

        public Dictionary<string, object>? KeyValuePairs { get; set; }

        public string? CreatedBy { get; set; }
    }
}
