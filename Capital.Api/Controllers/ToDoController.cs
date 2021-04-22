using Capital.Application.Commands;
using Capital.Application.Models;
using Capital.Application.Queries;
using Capital.Core.Entities;
using Capital.Core.Interfaces.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Capital.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ToDoController : ControllerBase
    {

        private readonly ILogger<ToDoController> _logger;
        private readonly IQueryDispatchService _queryDispatchService;
        private readonly ICommandDispatchService _commandDispatchService;

        public ToDoController(ILogger<ToDoController> logger, IQueryDispatchService queryDispatchService, ICommandDispatchService commandDispatchService)
        {
            _logger = logger;
            _queryDispatchService = queryDispatchService;
            _commandDispatchService = commandDispatchService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<ToDoModel>))]
        public async Task<IActionResult> GetToDo()
        {
            var result = await _queryDispatchService.DispatchAsync(new GetAllToDoQuery());
            return Ok(result.Result);
        }

        [HttpGet("{id}", Name = "GetToDo")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ToDoModel))]
        public async Task<IActionResult> GetToDo(int id)
        {
            var result = await _queryDispatchService.DispatchAsync(new GetToDoByIdQuery { Id = id });
            return Ok(result.Result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateToDo([FromBody] ToDoModel toDo)
        {
            var result = await _commandDispatchService.DispatchAsync(new CreateToDoCommand { ToDoModel = toDo });
            if (result != null && result.IsSuccess)
            {
                toDo.Id = result.Result;
                return CreatedAtRoute("GetToDo", new { id = toDo.Id }, toDo);
            }
            return BadRequest(result?.ErrorMessage);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateToDo(ToDoModel toDo)
        {
            var result = await _commandDispatchService.DispatchAsync(new UpdateToDoCommand { ToDoModel = toDo });
            return result != null && result.IsSuccess ? Ok() : BadRequest(result?.ErrorMessage);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RemoveToDo(int id)
        {
            var result = await _commandDispatchService.DispatchAsync(new RemoveToDoCommand { Id = id });
            return result != null && result.IsSuccess ? Ok() : BadRequest(result?.ErrorMessage);
        }
    }
}
