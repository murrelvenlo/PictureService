using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.ApiModels.Converter
{
    public class FormFileToByteArrayConverter : ITypeConverter<IFormFile, byte[]>
    {
        public byte[] Convert(IFormFile source, byte[] destination, ResolutionContext context)
        {
            using (var stream = new MemoryStream())
            {
                source.CopyTo(stream);
                return stream.ToArray();
            }
        }
    }
}
