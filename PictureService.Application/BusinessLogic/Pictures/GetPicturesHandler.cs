using MediatR;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class GetPicturesHandler : IRequestHandler<GetPictures, IEnumerable<Picture>>
    {
        private readonly IMongoRepository<Picture> _picturesRepository;

        public GetPicturesHandler(IMongoRepository<Picture> picturesRepository)
        {
            _picturesRepository = picturesRepository;
        }

        public async Task<IEnumerable<Picture>> Handle(GetPictures request, CancellationToken cancellationToken)
        {
            if (request.Ids.Count() == 1)
            {
                var pictureId = request.Ids.First();
                var picture = await _picturesRepository.FindByIdAsync(pictureId);
                return new List<Picture>() { picture };
            }

            return await _picturesRepository.GetAll();
        }
    }
}
