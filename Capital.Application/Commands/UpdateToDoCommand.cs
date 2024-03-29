﻿using Capital.Application.Common;
using Capital.Application.Mappers;
using Capital.Application.Models;
using Capital.Core.Entities;
using Capital.Core.Entities.Internal;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using System.Threading.Tasks;

namespace Capital.Application.Commands
{
    public class UpdateToDoCommand : ICommand<Output<bool>>
    {
        public ToDoModel ToDoModel { get; set; }
    }


    public class UpdateToDoCommandHandler : ICommandHandler<UpdateToDoCommand, Output<bool>>
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IArgumentValidator _argumentValidator;
        

        public UpdateToDoCommandHandler(IToDoRepository toDoRepository, IArgumentValidator argumentValidator)
        {
            _toDoRepository = toDoRepository;
            _argumentValidator = argumentValidator;
        }

        public async Task<Output<bool>> HandleAsync(UpdateToDoCommand command)
        {
            _argumentValidator.Validate(command);
            _argumentValidator.Validate(command.ToDoModel);            
            var result = await _toDoRepository.UpdateAsync(command.ToDoModel.ToTableModel());
            return new Output<bool>(result);
        }
    }
}
