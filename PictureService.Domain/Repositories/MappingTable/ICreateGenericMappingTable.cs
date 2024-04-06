using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.Domain.Repositories.MappingTable
{
    public interface ICreateGenericMappingTable : IBaseCreator<GenericMappingTable>
    {
        Task<GenericMappingTable> Store(GenericMappingTable entity);
    }
}
