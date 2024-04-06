using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class GetPictures : IQuery<IEnumerable<Picture>>
    {
        public GetPictures() : this(Enumerable.Empty<Guid>()) { } //Get all pictures
        public GetPictures(Guid id) : this(new List<Guid> { id }) { } //Get picture by id
        public GetPictures(IEnumerable<Guid> ids)
        {
            Ids = ids;
        }

        public IEnumerable<Guid> Ids;
    }
}
