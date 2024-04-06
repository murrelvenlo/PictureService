using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using PictureService.Domain.Attributes;
using PictureService.Domain.MongoDbHelper;

namespace PictureService.Domain.Models
{
    [BsonCollection("pictures")]
    public class Picture : Document
    {
        [BsonElement("name")]
        public string? Name { get; set; }
        [BsonElement("image_data")]
        public required byte[] ImageData { get; set; }
        [BsonElement("image_identifier")]
        public Guid ImageIdentifier { get; set; } = Guid.NewGuid();

        // Technical Fields
        [BsonElement("mapping_id")]
        public Guid? MappingId { get; set; }
        [BsonElement("created_by")]
        internal string CreatedBy { get; set; } = "PictureServiceAPI";
        [BsonElement("created_at")]
        internal DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        [BsonElement("modified_by")]
        internal string ModifiedBy { get; set; } = string.Empty;
        [BsonElement("modified_at")]
        internal DateTime ModifiedAtUtc { get; set; } = DateTime.UtcNow;
        [BsonElement("row_versions")]
        internal int RowVersions { get; set; } = 1;
    }
}
