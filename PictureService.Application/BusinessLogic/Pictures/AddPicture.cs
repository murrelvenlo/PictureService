using Microsoft.AspNetCore.Http;
using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class AddPicture : ICommand<Picture>
    {
        public AddPicture(string imageName, byte[] imageData, Guid mappingId)
        {
            ImageName = imageName;
            ImageData = imageData;
            MappingId = mappingId;
        }
        public string ImageName { get; set; }
        public byte[] ImageData { get; set; }
        public Guid MappingId { get; set; }
    }
}
