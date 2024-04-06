using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Equipments
{
    public class UpdateEquipment : ICommand<Equipment>
    {
        public UpdateEquipment(Equipment equipment)
        {
            Equipment = equipment;
        }

        public Equipment Equipment;
    }
}
