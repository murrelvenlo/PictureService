using FluentValidation;
using PictureService.Application.BusinessLogic.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.Validator
{
    public class GetPicturesValidator : AbstractValidator<GetPictures>
    {
        public GetPicturesValidator()
        {
            
        }
    }
}
