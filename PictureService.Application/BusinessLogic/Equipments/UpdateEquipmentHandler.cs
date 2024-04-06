using MediatR;
using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Equipments
{
    public class UpdateEquipmentHandler : IRequestHandler<UpdateEquipment, Equipment>
    {

        public Task<Equipment> Handle(UpdateEquipment request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
