using MediatR;
using Microsoft.AspNetCore.Http;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.MappingTable;
using PictureService.Domain.Repositories.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.Pictures
{
    public class AddPictureHandler : IRequestHandler<AddPicture, Picture>
    {
        private readonly IMongoRepository<Picture> _picturesRepository;
        private readonly IReadGenericMappingTables _mappingTablesReader;

        public AddPictureHandler(IMongoRepository<Picture> picturesRepository, IReadGenericMappingTables mappingTablesReader)
        {
            _picturesRepository = picturesRepository;
            _mappingTablesReader = mappingTablesReader;
        }

        public async Task<Picture> Handle(AddPicture request, CancellationToken cancellationToken)
        {
            //var mappingRecord = await _mappingTablesReader.ByEntityId(request.EntityId);
            //if (mappingRecord == null)
            //{
            //    throw new Exception("EntityId not found in the mapping table.");
            //}

            var mappingRecord = await _mappingTablesReader.ById(request.MappingId);
            if (mappingRecord == null)
            {
                throw new Exception("MappingId not found in the mapping table.");
            }

            var picture = new Picture
            {
                Name = request.ImageName,
                ImageData = request.ImageData,
                MappingId = mappingRecord.MappingId
            };

            await _picturesRepository.InsertOneAsync(picture);

            return _picturesRepository.FindById(picture.Id);
        }
    }
}
