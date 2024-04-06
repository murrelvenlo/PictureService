using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.ViewModels
{
    public class EquipmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public int HierarchyType { get; set; }
        public string Comment { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }
        public int? CompanyId { get; set; }
    }
}
