using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Domain.Attributes.Interface
{
    public interface IDocument
    {
        Guid Id { get; set; }
    }
}
