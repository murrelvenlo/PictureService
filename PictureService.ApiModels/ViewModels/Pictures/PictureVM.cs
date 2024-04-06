using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.ViewModels.Pictures
{
    public class PictureVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid ImageIdentifier { get; set; }
        public required byte[] ImageData { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAtUtc { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime ModifiedAtUtc { get; set; }
        public int RowVersion { get; set; }
        public Guid MappingId { get; set; }
    }
}
