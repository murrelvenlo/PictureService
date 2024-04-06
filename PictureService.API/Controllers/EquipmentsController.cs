using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureService.ApiModels.ViewModels;
using PictureService.ApiModels.ViewModels.Pictures;
using PictureService.Application.BusinessLogic.Equipments;
using PictureService.Application.BusinessLogic.Pictures;
using PictureService.Domain.Models;

namespace PictureService.API.Controllers
{
    [Route("api/equipments")]
    [ApiController]
    [Produces("application/json")]
    public class EquipmentsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public EquipmentsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EquipmentVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetEquipments());
            var resultVM = _mapper.Map<IEnumerable<EquipmentVM>>(result);
            return Ok(resultVM);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<EquipmentVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetEquipments(id));
            var resultVM = _mapper.Map<IEnumerable<EquipmentVM>>(result);
            return Ok(resultVM);
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PictureVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromForm] StorePictureVM storePictureVM)
        {
            if (storePictureVM == null)
                return BadRequest();

            var request = _mapper.Map<Picture>(storePictureVM);
            var result = await _mediator.Send(new AddPictureToEquipment(request.Name, request.ImageData, request.MappingId.Value, storePictureVM.EntityId));
            var resultVM = _mapper.Map<PictureVM>(result);
            return Ok(resultVM);
        }
    }
}
