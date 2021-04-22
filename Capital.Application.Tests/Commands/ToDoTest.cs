using Capital.Application.Commands;
using Capital.Application.Common;
using Capital.Application.Validators;
using Capital.Core.Entities;
using Capital.Core.Entities.Internal;
using Capital.Core.Enums;
using Capital.Core.Interfaces.Common;
using Capital.Core.Interfaces.Handlers;
using Capital.Core.Interfaces.Infrastructure;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Capital.Application.Tests.Commands
{

    public class ToDoTest
    {

        private readonly Mock<IToDoRepository> _toDoReposiotry;
        private readonly Mock<IArgumentValidator> _argumentValidator;
        public ToDoTest()
        {
            _toDoReposiotry = new Mock<IToDoRepository>();
            _argumentValidator = new Mock<IArgumentValidator>();
        }

        [Fact]
        public async Task ToDo_CreateCommand_Null_Input()
        {
            //arrange
            _ = _argumentValidator.Setup(x => x.Validate(It.IsAny<object>())).Throws<ArgumentNullException>();
            //act
            var _commandHandler = new CreateNewToDoCommandHandler(_toDoReposiotry.Object, _argumentValidator.Object);
            Func<Task> act = async () => await _commandHandler.HandleAsync(null);
            //assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task ToDo_CreateCommand_With_Null_ToDo()
        {
            //arrange
            var createToDoCommand = new CreateToDoCommand
            {
                ToDoModel = null,
            };
            _ = _argumentValidator.Setup(x => x.Validate(It.IsAny<object>())).Throws<ArgumentNullException>();
            //act
            var _commandHandler = new CreateNewToDoCommandHandler(_toDoReposiotry.Object, _argumentValidator.Object);
            Func<Task> act = async () => await _commandHandler.HandleAsync(createToDoCommand);
            //assert
            await act.Should().ThrowAsync<ArgumentNullException>().WithMessage("Value cannot be null.");
        }

        [Fact]
        public async Task ToDo_CreateCommand_With_Valid_ToDo()
        {
            //arrange
            var createToDoCommand = new CreateToDoCommand
            {
                ToDoModel = new Models.ToDoModel
                {
                    Title = "Test Todo",
                    StatusId = (int)ToDoItemStatus.New
                },
            };
            _ = _toDoReposiotry.Setup( x=>x.AddAsync(It.IsAny<ToDoItem>()).Result).Returns(1);
            //act
            var _commandHandler = new CreateNewToDoCommandHandler(_toDoReposiotry.Object, _argumentValidator.Object);

            var result = await _commandHandler.HandleAsync(createToDoCommand);
            //assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeTrue();
            result.Result.Should().Be(1);            
        }

        [Fact]
        public async Task ToDo_CreateCommand_With_Valid_ButNotSetStatus_ToDo()
        {
            //arrange
            var createToDoCommand = new CreateToDoCommand
            {
                ToDoModel = new Models.ToDoModel
                {
                    Title = "Test Todo"
                },
            };
            _ = _toDoReposiotry.Setup(x => x.AddAsync(It.IsAny<ToDoItem>()).Result).Returns(0);
            //act
            var _commandHandler = new CreateNewToDoCommandHandler(_toDoReposiotry.Object, _argumentValidator.Object);

            var result = await _commandHandler.HandleAsync(createToDoCommand);
            //assert
            result.Should().NotBeNull();
            result.IsSuccess.Should().BeFalse();
        }
    }
}
