using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PictureService.Domain.Models;

namespace PictureService.Application.BusinessLogic.Equipments
{
    public class GetEquipments : IQuery<IEnumerable<Equipment>>
    {
        public GetEquipments() : this(Enumerable.Empty<int>()) { }
        public GetEquipments(int id) : this(new List<int> { id }) { }

        public GetEquipments(IEnumerable<int> ids)
        {
            Ids = ids;
        }

        public IEnumerable<int> Ids;
    }
}
