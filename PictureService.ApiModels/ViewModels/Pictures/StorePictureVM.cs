using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.ViewModels.Pictures
{
    public class StorePictureVM
    {
        public string Name { get; set; } = string.Empty;
        public Guid ImageIdentifier { get; set; }
        //public byte[] ImageData { get; set; }
        //public string CreatedBy { get; set; }
        public required IFormFile ImageData { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public int RowVersion { get; set; }
        public Guid MappingId { get; set; }
        public string? CreatedBy { get; set; }

        //Extra field for adding Picture to Equipment
        public int EntityId { get; set; }
        //public string TableName { get; set; }
    }
}
