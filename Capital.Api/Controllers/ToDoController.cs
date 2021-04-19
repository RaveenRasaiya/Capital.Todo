using Capital.Application.Commands;
using Capital.Application.Queries;
using Capital.Core.Interfaces.Common;
using Capital.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Capital.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Get()
        {
            var result = await _queryDispatchService.DispatchAsync(new GetAllToDoQuery());
            return Ok(result.Result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ToDoItem toDoItem)
        {
            await _commandDispatchService.DispatchAsync(new CreateNewToDoCommand { ToDoItem = toDoItem });
            return Ok();
        }
    }
}
