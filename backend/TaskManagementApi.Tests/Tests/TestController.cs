using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Controllers;
using TaskManagementApi.Data;
using TaskManagementApi.DTOs;
using TaskManagementApi.Models;
using Xunit;
using TaskStatus = TaskManagementApi.Models.TaskStatus;
namespace TaskManagementApi.Tests
{
    public class TasksControllerTests
    {
        private AppDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new AppDbContext(options);

            context.Tasks.Add(new TaskItem
            {
                Id = 1,
                Title = "Test Task",
                Description = "Test Description",
                Status = TaskStatus.Todo,
                Priority = TaskPriority.Medium
            });

            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetTasks_ReturnsAllTasks()
        {
            // Arrange
            var context = GetDbContext();
            var controller = new TasksController(context);

            // Act
            var result = await controller.GetTasks(null, null, null, null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var tasks = Assert.IsAssignableFrom<IEnumerable<TaskItem>>(okResult.Value);

            Assert.Single(tasks);
        }

        [Fact]
        public async Task GetTask_ReturnsTask_WhenTaskExists()
        {
            var context = GetDbContext();
            var controller = new TasksController(context);

            var result = await controller.GetTask(1);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var task = Assert.IsType<TaskItem>(okResult.Value);

            Assert.Equal("Test Task", task.Title);
        }

        [Fact]
        public async Task GetTask_ReturnsNotFound_WhenTaskDoesNotExist()
        {
            var context = GetDbContext();
            var controller = new TasksController(context);

            var result = await controller.GetTask(999);

            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public async Task CreateTask_CreatesNewTask()
        {
            var context = GetDbContext();
            var controller = new TasksController(context);

            var dto = new CreateTaskDto
            {
                Title = "New Task",
                Description = "Test",
                Priority = TaskPriority.High
            };

            var result = await controller.CreateTask(dto);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var task = Assert.IsType<TaskItem>(createdResult.Value);

            Assert.Equal("New Task", task.Title);
        }

        [Fact]
        public async Task DeleteTask_RemovesTask()
        {
            var context = GetDbContext();
            var controller = new TasksController(context);

            var result = await controller.DeleteTask(1);

            Assert.IsType<NoContentResult>(result);
        }
    }
}