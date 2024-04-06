using MongoDB.Bson;
using PictureService.Domain.Attributes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Domain.MongoDbHelper
{
    public class Document : IDocument
    {
        public Guid Id { get; set; }
    }
}
