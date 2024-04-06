using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.ViewModels.MappingTable
{
    public class MappingTableVM
    {
        public Guid MappingId { get; set; }

        public string? TableName { get; set; }
        public int? EntityId { get; set; }
        [JsonIgnore]
        public string? KeyValuePairs { get; set; }
        // Deserialize KeyValuePairs from JSON string to Dictionary, removing backslashes if present
        public Dictionary<string, object>? KeyValuePairsDict
        {
            get
            {
                if (KeyValuePairs == null)
                    return null;

                try
                {
                    return JsonConvert.DeserializeObject<Dictionary<string, object>>(KeyValuePairs);
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it)
                    // You can also throw a custom exception or return null
                    Console.WriteLine($"Error deserializing KeyValuePairs: {ex.Message}");
                    return null;
                }
            }
        }


        // Technical Fields
        internal string CreatedBy { get; set; } = string.Empty;
        internal DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        internal string ModifiedBy { get; set; } = string.Empty;
        internal DateTime ModifiedAtUtc { get; set; } = DateTime.UtcNow;
        internal int RowVersions { get; set; }
    }
}
