using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class AddPictureToEquipment : ICommand<Picture>
    {
        public AddPictureToEquipment(string imageName, byte[] imageData, Guid mappingId, int entityId)
        {
            ImageName = imageName;
            ImageData = imageData;
            MappingId = mappingId;
            EntityId = entityId;
        }

        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public Guid MappingId { get; set; }
        public int EntityId { get; set; }
    }
}
