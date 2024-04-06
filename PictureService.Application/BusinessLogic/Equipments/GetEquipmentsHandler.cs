using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.Equipments;

namespace PictureService.Application.BusinessLogic.Equipments
{
    public class GetEquipmentsHandler : IRequestHandler<GetEquipments, IEnumerable<Equipment>>
    {
        private readonly IReadEquipments _equipmentsReader;

        public GetEquipmentsHandler(IReadEquipments equipmentsReader)
        {
            _equipmentsReader = equipmentsReader;
        }

        public async Task<IEnumerable<Equipment>> Handle(GetEquipments request, CancellationToken cancellationToken)
        {
            if (!request.Ids.Any())
                return await _equipmentsReader.All();

            if (!request.Ids.Skip(1).Any())
                return new List<Equipment>() { await _equipmentsReader.ById(request.Ids.First()) };

            return await _equipmentsReader.MultipleById(request.Ids);
        }
    }
}
