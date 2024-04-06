using System;
using System.Collections.Generic;

namespace PictureService.Domain.Models;

public partial class GenericMappingTable
{
    public Guid MappingId { get; set; }

    public string? TableName { get; set; }

    public int? EntityId { get; set; }

    public string? KeyValuePairs { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedAtUtc { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedAtUtc { get; set; }

    public int? RowVersion { get; set; }
}
