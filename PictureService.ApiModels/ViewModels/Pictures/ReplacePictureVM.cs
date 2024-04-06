using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.ViewModels.Pictures
{
    public class ReplacePictureVM
    {
        public string Name { get; set; } = string.Empty;
        public required IFormFile ImageData { get; set; }
    }
}
