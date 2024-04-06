using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureService.ApiModels.ViewModels.Pictures;
using PictureService.Application.BusinessLogic.Pictures;
using PictureService.Domain.Models;
using PictureService.Domain.Repositories.Pictures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.API.Controllers
{
    [Route("api/pictures")]
    [ApiController]
    [Produces("application/json")]
    public class PicturesController : Controller
    {
        private readonly IMongoRepository<Picture> _picturesRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PicturesController(IMongoRepository<Picture> picturesRepository, IMediator mediator, IMapper mapper)
        {
            _picturesRepository = picturesRepository;
            _mediator = mediator;
            _mapper = mapper;
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
            var result = await _mediator.Send(new AddPicture(request.Name, request.ImageData, request.MappingId.Value));
            var resultVM = _mapper.Map<PictureVM>(result);
            return CreatedAtAction(nameof(GetById), new { id = resultVM.Id }, resultVM);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PictureVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetPictures());
            var resultVM = _mapper.Map<IEnumerable<PictureVM>>(results);
            return Ok(resultVM);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PictureVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetPictures(id));
            var resultVM = _mapper.Map<PictureVM>(result.First());
            return Ok(resultVM);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PictureVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, [FromForm] ReplacePictureVM? replacePictureVM)
        {
            if (replacePictureVM is null)
                return BadRequest();

            var picture = _mapper.Map<Picture>(replacePictureVM);
            picture.Id = id;

            var result = await _mediator.Send(new UpdatePicture(picture));
            var resultVM = _mapper.Map<PictureVM>(result);
            return Ok(resultVM);
        }

    }
}
