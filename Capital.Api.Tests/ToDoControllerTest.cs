using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Numerics;
using Capital.Application.Models;
using Capital.Core.Enums;

namespace Capital.Api.Tests
{
    public class ToDoControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private const string testCategory = "Integration";
        private readonly WebApplicationFactory<Startup> _factory;
        private const string endPointUrl = "/api/v1/todo/";
        private const string mediaType = "application/json";

        public ToDoControllerTest(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }


        [Fact]
        [Trait("Category", testCategory)]
        public async Task ToDoController_Get_All_ToDo_NoContent()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}";
            var response = await client.GetAsync(url);

            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }


        [Fact]
        [Trait("Category", testCategory)]
        public async Task ToDoController_Create_NewToDo()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}";
            var content = new StringContent(JsonConvert.SerializeObject(new ToDoModel
            {
                StatusId = (int)ToDoItemStatus.New,
                Title = "Test ToDo"
            }), Encoding.UTF8, mediaType);

            // Act
            var response = await client.PostAsync(url, content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var httpResponseMessage = await client.GetAsync(url);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var toDoModels = JsonConvert.DeserializeObject<IEnumerable<ToDoModel>>(stringResponse);
            toDoModels.Should().NotBeNull();
            toDoModels.Should().HaveCount(1);
        }

        [Fact]
        [Trait("Category", testCategory)]
        public async Task ToDoController_Update_ToDo()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}";
            var content = new StringContent(JsonConvert.SerializeObject(new ToDoModel
            {
                StatusId = (int)ToDoItemStatus.New,
                Description = "Test ToDo",
                Title = "Test ToDo"
            }), Encoding.UTF8, mediaType);

            // Act
            var response = await client.PostAsync(url, content);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var httpResponseMessage = await client.GetAsync(url);

            httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.OK);

            var stringResponse = await httpResponseMessage.Content.ReadAsStringAsync();
            var toDoModels = JsonConvert.DeserializeObject<IEnumerable<ToDoModel>>(stringResponse);
            toDoModels.Should().NotBeNull();
            toDoModels.Should().HaveCount(1);
        }



        [Fact]
        [Trait("Category", testCategory)]

        public async Task ToDoController_Post_Not_Send_Request_Parameter()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}";

            var content = new StringContent(string.Empty, Encoding.UTF8, mediaType);
            // Act
            var response = await client.PostAsync(url, content);
            // Assert            
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        [Trait("Category", testCategory)]
        public async Task ToDoController_Create_NewToDo_No_Title()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}";
            var content = new StringContent(JsonConvert.SerializeObject(new ToDoModel
            {
                StatusId = (int)ToDoItemStatus.New,
            }), Encoding.UTF8, mediaType);

            // Act
            var response = await client.PostAsync(url, content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

            var stringResponse = await response.Content.ReadAsStringAsync();
            stringResponse.Should().NotBeNullOrEmpty();

            ErrorModel errorModel = JsonConvert.DeserializeObject<ErrorModel>(stringResponse);
            errorModel.Should().NotBeNull();
            errorModel.Message.Should().Contain("Mandatory parameter missing Title");
        }


        [Fact]
        [Trait("Category", testCategory)]
        public async Task ToDoController_Create_NewToDo_No_Status()
        {
            // Arrange
            var client = _factory.CreateClient();

            string url = $"{endPointUrl}";
            var content = new StringContent(JsonConvert.SerializeObject(new ToDoModel
            {
                Title = "There is no status",
            }), Encoding.UTF8, mediaType);

            // Act
            var response = await client.PostAsync(url, content);

            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
