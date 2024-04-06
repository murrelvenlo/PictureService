using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class UpdatePicture : ICommand<Picture>
    {
        public UpdatePicture(Picture picture)//, bool resetLastExecutionTime = false
        {
            Picture = picture;
            //ResetLastExecutionTime = resetLastExecutionTime;
        }
        public Picture Picture;
        //public bool ResetLastExecutionTime;
    }
}
