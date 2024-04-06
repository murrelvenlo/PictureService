namespace PictureService.Domain.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public int HierarchyType { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public int? CompanyId { get; set; }

        // Technical Fields
        internal string CreatedBy { get; set; } = string.Empty;
        internal DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        internal string ModifiedBy { get; set; } = string.Empty!;
        internal DateTime ModifiedAtUtc { get; set; } = DateTime.UtcNow;
        internal int RowVersions { get; set; } = 1;
    }
}
