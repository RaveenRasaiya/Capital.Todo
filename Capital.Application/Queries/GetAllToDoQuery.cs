using Capital.Application.Common;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using Capital.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capital.Application.Queries
{
    public class GetAllToDoQuery : IQuery<Output<IEnumerable<ToDoItem>>>
    {

    }

    public class GetAllToDoQueryHandler : IQueryHandler<GetAllToDoQuery, Output<IEnumerable<ToDoItem>>>
    {
        private readonly IToDoRepository _toDoRepository;

        public GetAllToDoQueryHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<Output<IEnumerable<ToDoItem>>> HandleAsync(GetAllToDoQuery query)
        {
            var result = await _toDoRepository.GetAll();
            if (result == null || !result.Any())
            {
                return new Output<IEnumerable<ToDoItem>>(false);
            }
            return new Output<IEnumerable<ToDoItem>>(true, result);
        }
    }
}
