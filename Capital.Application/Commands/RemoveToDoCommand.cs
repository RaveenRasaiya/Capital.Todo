using Capital.Application.Common;
using Capital.Core.Entities.Internal;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Capital.Application.Commands
{
    public class RemoveToDoCommand : ICommand<Output<bool>>
    {
        public int Id { get; set; }
    }

    public class RemoveToDoCommandHandler : ICommandHandler<RemoveToDoCommand, Output<bool>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IArgumentValidator _argumentValidator;

        public RemoveToDoCommandHandler(IToDoRepository toDoRepository,IArgumentValidator argumentValidator)
        {
            _toDoRepository = toDoRepository;
            _argumentValidator = argumentValidator;
        }

        public async Task<Output<bool>> HandleAsync(RemoveToDoCommand command)
        {
            _argumentValidator.Validate(command);
            var toDoItem = await _toDoRepository.GetByIdAsync(command.Id);
            if (toDoItem == null)
            {
                return new Output<bool>(false);
            }          
            var result = await _toDoRepository.RemoveAsync(toDoItem);
            return new Output<bool>(result);
        }
    }
}
