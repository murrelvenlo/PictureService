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
    public class GetMappingTablesHandler : IRequestHandler<GetMappingTables, IEnumerable<GenericMappingTable>>
    {
        private readonly IReadGenericMappingTables _mappingTablesReader;

        public GetMappingTablesHandler(IReadGenericMappingTables mappingTablesReader)
        {
            _mappingTablesReader = mappingTablesReader;
        }

        public async Task<IEnumerable<GenericMappingTable>> Handle(GetMappingTables request, CancellationToken cancellationToken)
        {
            if (!request.Ids.Any() && !request.TableNames.Any())
                return await _mappingTablesReader.All();

            if (request.TableNames.Any())
            {
                var tables = new List<GenericMappingTable>();

                foreach (var tableName in request.TableNames)
                {
                    var table = await _mappingTablesReader.ByName(tableName);
                    if (table != null)
                    {
                        tables.Add(table);
                    }
                }
                return tables;
            }

            if (!request.Ids.Skip(1).Any())
                return new List<GenericMappingTable>() { await _mappingTablesReader.ById(request.Ids.First()) };


            return await _mappingTablesReader.MultipleById(request.Ids);
        }
    }
}
