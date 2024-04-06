using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureService.ApiModels.ViewModels.MappingTable;
using PictureService.Application.BusinessLogic.MappingTable;
using PictureService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureService.API.Controllers
{
    [Route("api/mappings")]
    [ApiController]
    [Produces("application/json")]
    public class GenericMappingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GenericMappingsController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(MappingTableVM))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Add([FromBody] AddMappingVM addMappingVM)
        {
            if (addMappingVM == null)
                return BadRequest();

            var request = _mapper.Map<GenericMappingTable>(addMappingVM);
            var result = await _mediator.Send(new AddMappingTable(request.TableName, request.EntityId, request.KeyValuePairs));
            var resultVM = _mapper.Map<MappingTableVM>(result);
            return CreatedAtAction(nameof(GetById), new { id = resultVM.MappingId }, resultVM);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MappingTableVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var results = await _mediator.Send(new GetMappingTables());
            var resultVM = _mapper.Map<IEnumerable<MappingTableVM>>(results);
            return Ok(resultVM);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MappingTableVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var results = await _mediator.Send(new GetMappingTables(id));
            var resultVM = _mapper.Map<IEnumerable<MappingTableVM>>(results);
            return Ok(resultVM);
        }

        [HttpGet("{entityId}/{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MappingTableVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByEntityIdAndTableName(int entityId, string tableName)
        {
            var results = await _mediator.Send(new GetEntity(entityId, tableName));
            var resultVM = _mapper.Map<IEnumerable<MappingTableVM>>(results);
            return Ok(resultVM);
        }

        [HttpGet("{tableName}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<MappingTableVM>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTableByName(string tableName)
        {
            var result = await _mediator.Send(new GetMappingTables(tableName));
            var resultVM = _mapper.Map<MappingTableVM>(result.First());
            return Ok(resultVM);
        }

    }
}
