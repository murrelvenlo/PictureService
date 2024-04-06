using MediatR;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.MappingTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.MappingTable
{
    public class GetEntityHandler : IRequestHandler<GetEntity, IEnumerable<GenericMappingTable>>
    {
        private readonly IReadGenericMappingTables _mappingTablesReader;

        public GetEntityHandler(IReadGenericMappingTables mappingTablesReader)
        {
            _mappingTablesReader = mappingTablesReader;
        }

        public async Task<IEnumerable<GenericMappingTable>> Handle(GetEntity request, CancellationToken cancellationToken)
        {
            return new List<GenericMappingTable>() { await _mappingTablesReader.ByEntityIdAndTableName(request.EntityId, request.TableName) };
        }
    }
}
