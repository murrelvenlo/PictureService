using MediatR;
using PictureService.Domain.Exceptions;
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
    public class AddPictureToEquipmentHandler : IRequestHandler<AddPictureToEquipment, Picture>
    {
        private readonly IMongoRepository<Picture> _picturesRepository;
        private readonly IReadGenericMappingTables _mappingTablesReader;
        private readonly AddPictureHandler _addPictureHandler;
        private readonly IMediator _mediator;

        public AddPictureToEquipmentHandler(IMongoRepository<Picture> picturesRepository,
            IReadGenericMappingTables mappingTablesReader,
            AddPictureHandler addPictureHandler,
            IMediator mediator)
        {
            _mappingTablesReader = mappingTablesReader;
            _picturesRepository = picturesRepository;
            _addPictureHandler = addPictureHandler;
            _mediator = mediator;
        }

        public async Task<Picture> Handle(AddPictureToEquipment request, CancellationToken cancellationToken)
        {
            var mappingRecord = await _mappingTablesReader.ByMappingIdAndEntityId(request.EntityId, request.MappingId);
            if (mappingRecord == null)
            {
                throw new BusinessLogicException("MappingId not found in the mapping table.");
            }

            // Step 2: Add the picture using the AddPictureHandler
            var picture = await _mediator.Send(new AddPicture(request.ImageName, request.ImageData, mappingRecord.MappingId));

            return picture;
        }

        //public async Task<Picture> Handle(AddPictureToEquipment request, CancellationToken cancellationToken)
        //{
        //    var mappingRecord = await _mappingTablesReader.ByEntityIdAndTableName(request.EntityId, request.TableName);
        //    if (mappingRecord == null)
        //    {
        //        throw new BusinessLogicException("MappingId not found in the mapping table.");
        //    }

        //    // Step 2: Add the picture using the AddPictureHandler
        //    var picture = await _mediator.Send(new AddPicture(request.ImageName, request.ImageData, mappingRecord.MappingId));

        //    return picture;
        //}
    }
}
