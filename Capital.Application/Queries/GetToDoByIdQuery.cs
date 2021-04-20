using Capital.Application.Common;
using Capital.Application.Mappers;
using Capital.Application.Models;
using Capital.Core.Entities;
using Capital.Core.Entities.Internal;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Capital.Application.Queries
{
    public class GetToDoByIdQuery : IQuery<Output<ToDoModel>>
    {
        public int Id { get; set; }
    }

    public class GetToDoByIdQueryHandler : IQueryHandler<GetToDoByIdQuery, Output<ToDoModel>>
    {
        private readonly IToDoRepository _toDoRepository;

        public GetToDoByIdQueryHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<Output<ToDoModel>> HandleAsync(GetToDoByIdQuery query)
        {
            var result = await _toDoRepository.GetByIdAsync(query.Id);
            if (result == null)
            {
                return new Output<ToDoModel>(false);
            }
            return new Output<ToDoModel>(true, result.ToViewModel());
        }
    }
}
