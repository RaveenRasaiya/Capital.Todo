using Capital.Application.Common;
using Capital.Application.Mappers;
using Capital.Application.Models;
using Capital.Core.Entities.Internal;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Capital.Application.Commands
{
    public class CreateToDoCommand : ICommand<Output<int>>
    {
        public ToDoModel ToDoModel { get; set; }
    }

    public class CreateNewToDoCommandHandler : ICommandHandler<CreateToDoCommand, Output<int>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IArgumentValidator _argumentValidator;
      

        public CreateNewToDoCommandHandler(IToDoRepository toDoRepository, IArgumentValidator argumentValidator)
        {
            _toDoRepository = toDoRepository;
            _argumentValidator = argumentValidator;
      
        }

        public async Task<Output<int>> HandleAsync(CreateToDoCommand command)
        {
            _argumentValidator.Validate(command);
            _argumentValidator.Validate(command.ToDoModel);

            var result = await _toDoRepository.AddAsync(command.ToDoModel.ToTableModel());
            var isSuccess = result > 0;
            return new Output<int>(isSuccess, result);
        }
    }
}
