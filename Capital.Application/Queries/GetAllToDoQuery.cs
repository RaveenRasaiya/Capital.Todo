using Capital.Application.Common;
using Capital.Application.Mappers;
using Capital.Application.Models;
using Capital.Core.Entities;
using Capital.Core.Entities.Internal;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Capital.Application.Queries
{
    public class GetAllToDoQuery : IQuery<Output<IEnumerable<ToDoModel>>>
    {

    }

    public class GetAllToDoQueryHandler : IQueryHandler<GetAllToDoQuery, Output<IEnumerable<ToDoModel>>>
    {
        private readonly IToDoRepository _toDoRepository;

        public GetAllToDoQueryHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<Output<IEnumerable<ToDoModel>>> HandleAsync(GetAllToDoQuery query)
        {
            var result = await _toDoRepository.GetAllAsync();
            if (result == null || !result.Any())
            {
                return new Output<IEnumerable<ToDoModel>>(false);
            }
            return new Output<IEnumerable<ToDoModel>>(true, result.ToViewModel());
        }
    }
}
