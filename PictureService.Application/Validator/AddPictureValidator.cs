using FluentValidation;
using Microsoft.AspNetCore.Http;
using PictureService.Application.BusinessLogic.Pictures;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.Validator
{
    public class AddPictureValidator : AbstractValidator<AddPicture>
    {
        public AddPictureValidator(IMongoRepository<Picture> picturesRepository)
        {

            RuleFor(x => x.ImageName)
                .NotEmpty().WithMessage("Image name cannot be empty.")
                .MaximumLength(200).WithMessage("Image name must be less than 200 characters.")
                .Must((instance, name) => !picturesRepository.FilterBy(p => p.Name == name).Any()).WithMessage("Image name already exists.");

            RuleFor(x => x.ImageData)
                .NotNull().WithMessage("Please select a file.")
                .Must((instance, image) => !picturesRepository.FilterBy(p => p.ImageData == image).Any()).WithMessage("Image data already exists.");
        }
    }
}
