using Capital.Application.Common;
using Capital.Application.Validators;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using Capital.Core.Models;
using System.Threading.Tasks;

namespace Capital.Application.Commands
{
    public class CreateNewToDoCommand : ICommand<Output<int>>
    {
        public ToDoItem ToDoItem { get; set; }
    }

    public class CreateNewToDoCommandHandler : ICommandHandler<CreateNewToDoCommand, Output<int>>
    {
        private readonly IToDoRepository _toDoRepository;

        public CreateNewToDoCommandHandler(IToDoRepository toDoRepository)
        {
            _toDoRepository = toDoRepository;
        }

        public async Task<Output<int>> HandleAsync(CreateNewToDoCommand command)
        {
            ArgumentValidator.NotNull(command);
            ArgumentValidator.NotNull(command.ToDoItem);
            var result = await _toDoRepository.Add(command.ToDoItem);
            var isSuccess = result > 0;
            return new Output<int>(isSuccess, result);
        }
    }
}
