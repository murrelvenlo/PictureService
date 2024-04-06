using MediatR;
using PictureService.Domain.Exceptions;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.MappingTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Application.BusinessLogic.MappingTable
{
    public class AddMappingTableHandler : IRequestHandler<AddMappingTable, GenericMappingTable>
    {
        private readonly ICreateGenericMappingTable _mappingTableCreator;
        private readonly IReadGenericMappingTables _mappingTablesReader;

        public AddMappingTableHandler(ICreateGenericMappingTable mappingTableCreator, IReadGenericMappingTables mappingTablesReader)
        {
            _mappingTableCreator = mappingTableCreator;
            _mappingTablesReader = mappingTablesReader;
        }

        public async Task<GenericMappingTable> Handle(AddMappingTable request, CancellationToken cancellationToken)
        {
            //verify if table exists
            bool tableExists = await _mappingTablesReader.Exist(request.TableName);

            if (!tableExists)
                throw new BusinessLogicException("The specified table does not exist in the database.");



            var id = await _mappingTableCreator.Add(new GenericMappingTable()
            {
                TableName = request.TableName,
                EntityId = request.EntityId,
                KeyValuePairs = request.KeyValuePairs,
            });

            return await _mappingTablesReader.ById(id);
        }

        //public async Task<GenericMappingTable> Handle(AddMappingTable request, CancellationToken cancellationToken)
        //{
        //    //verify if table exists
        //    bool tableExists = await _mappingTablesReader.Exist(request.TableName);

        //    if (!tableExists)
        //        throw new BusinessLogicException("The specified table does not exist in the database.");

        //    var table = new GenericMappingTable
        //    {
        //        TableName = request.TableName,
        //        EntityId = request.EntityId,
        //        KeyValuePairs = request.KeyValuePairs,
        //    };

        //    await _mappingTableCreator.Store(table);

        //    return await _mappingTablesReader.ById(table.MappingId);
        //}
    }
}
