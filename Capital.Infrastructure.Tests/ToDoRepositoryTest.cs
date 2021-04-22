using Capital.Core.Interfaces.Infrastructure;
using Capital.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;

namespace Capital.Infrastructure.Tests
{
    public class ToDoRepositoryTest
    {
        private readonly DbContextOptions<ToDoDbContext> _options;
        private readonly IToDoRepository _toDoRepository;
        public ToDoRepositoryTest()
        {
            _options = new DbContextOptionsBuilder<ToDoDbContext>()
            .UseInMemoryDatabase(databaseName: "ToDoDb_UT")
                .Options;
            var context = new ToDoDbContext(_options);
            _toDoRepository = new ToDoRepository(context);
        }

        [Fact]
        public async Task ToDo_Create_New_Entry()
        {
            var result = await _toDoRepository.AddAsync(new Core.Entities.ToDoItem
            {
                Title = "Test",
                CreatedOn = DateTime.Now,
                Status = Core.Enums.ToDoItemStatus.New
            });

            result.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task ToDo_Get_ToDos()
        {
            //act
            await _toDoRepository.AddAsync(new Core.Entities.ToDoItem
            {
                Title = "Test",
                CreatedOn = DateTime.Now,
                Status = Core.Enums.ToDoItemStatus.New
            });
            //act
            await _toDoRepository.AddAsync(new Core.Entities.ToDoItem
            {
                Title = "Test",
                CreatedOn = DateTime.Now,
                Status = Core.Enums.ToDoItemStatus.New
            });

            var result = await _toDoRepository.GetAllAsync();
            result.Should().NotBeNull();
            result.Should().NotBeEmpty();
            result.Should().HaveCount(3);
        }


        [Fact]
        public async Task ToDo_Get_By_Id()
        {
            //act
            var result = await _toDoRepository.AddAsync(new Core.Entities.ToDoItem
            {
                Title = "Test",
                CreatedOn = DateTime.Now,
                Status = Core.Enums.ToDoItemStatus.New
            });
            result.Should().BeGreaterThan(0);
            var toDoItem = await _toDoRepository.GetByIdAsync(result);
            toDoItem.Should().NotBeNull();
        }


        [Fact]
        public async Task ToDo_Create_And_Update_Entry()
        {
            var toDoItem = new Core.Entities.ToDoItem
            {
                Title = "Test",
                CreatedOn = DateTime.Now,
                Status = Core.Enums.ToDoItemStatus.New
            };
            var result = await _toDoRepository.AddAsync(toDoItem);

            result.Should().BeGreaterThan(0);
            toDoItem.Id = result;
            toDoItem.Title = "Changed for Update";
            await _toDoRepository.UpdateAsync(toDoItem);

            var updatedToDoItem = await _toDoRepository.GetByIdAsync(result);

            updatedToDoItem.Should().NotBeNull();
            updatedToDoItem.Title.Should().Be("Changed for Update");

        }
    }
}
