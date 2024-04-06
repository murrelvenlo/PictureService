using MediatR;
using PictureService.Domain.Exceptions;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class UpdatePictureHandler : IRequestHandler<UpdatePicture, Picture>
    {
        private readonly IMongoRepository<Picture> _pictureRepository;

        public UpdatePictureHandler(IMongoRepository<Picture> pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        public async Task<Picture> Handle(UpdatePicture request, CancellationToken cancellationToken)
        {
            var picture = request.Picture;
            var id = picture.Id;

            if (id == Guid.Empty)
                throw new BusinessLogicException("Cannot update picture. Missing or invalid Id");

            var existingPicture = await _pictureRepository.FindByIdAsync(id);
            if (existingPicture == null)
                throw new BusinessLogicException($"Picture with Id {id} not found");

            // Update the properties of the existing picture
            existingPicture.Name = picture.Name;
            existingPicture.ImageData = picture.ImageData;

            await _pictureRepository.ReplaceOneAsync(existingPicture);

            return _pictureRepository.FindById(existingPicture.Id);
        }
    }
}
