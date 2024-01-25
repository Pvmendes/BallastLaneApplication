using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Core.Interfaces.Repository;
using BallastLaneApplication.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLaneApplication.Tests
{
    public class TaskServiceTests
    {
        private Mock<ITaskRepository> _taskRepositoryMock;
        private TaskService _taskService;

        [SetUp]
        public void Setup()
        {
            _taskRepositoryMock = new Mock<ITaskRepository>();
            _taskService = new TaskService(_taskRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllTasksAsync_TasksExist_ReturnsAllTasks()
        {
            // Arrange
            var tasks = new List<TaskEntitie>
            {
                new TaskEntitie { Id = Guid.NewGuid(), Title = "Test Task 1" },
                new TaskEntitie { Id = Guid.NewGuid(), Title = "Test Task 2" }
            };
            _taskRepositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(tasks);

            // Act
            var result = await _taskService.GetAllTasksAsync();

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result, Is.EqualTo(tasks));
        }

        [Test]
        public async Task GetTaskByIdAsync_TaskExists_ReturnsTask()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            var expectedTask = new TaskEntitie { Id = taskId, Title = "Test Task" };
            _taskRepositoryMock.Setup(repo => repo.GetByIdAsync(taskId)).ReturnsAsync(expectedTask);

            // Act
            var result = await _taskService.GetTaskByIdAsync(taskId);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(expectedTask));
        }

        [Test]
        public async Task CreateTaskAsync_ValidTask_AddsTask()
        {
            // Arrange
            var newTask = new TaskEntitie { Title = "New Task" };
            _taskRepositoryMock.Setup(repo => repo.AddAsync(newTask)).ReturnsAsync(newTask);

            // Act
            var result = await _taskService.CreateTaskAsync(newTask);

            // Assert
            Assert.IsNotNull(result);
            _taskRepositoryMock.Verify(repo => repo.AddAsync(newTask), Times.Once);
        }

        [Test]
        public async Task UpdateTaskAsync_TaskExists_UpdatesTask()
        {
            // Arrange
            var existingTask = new TaskEntitie { Id = Guid.NewGuid(), Title = "Existing Task" };
            _taskRepositoryMock.Setup(repo => repo.UpdateAsync(existingTask)).Returns(Task.CompletedTask);

            // Act
            await _taskService.UpdateTaskAsync(existingTask);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.UpdateAsync(existingTask), Times.Once);
        }

        [Test]
        public async Task DeleteTaskAsync_TaskExists_DeletesTask()
        {
            // Arrange
            var taskId = Guid.NewGuid();
            _taskRepositoryMock.Setup(repo => repo.DeleteAsync(taskId)).Returns(Task.CompletedTask);

            // Act
            await _taskService.DeleteTaskAsync(taskId);

            // Assert
            _taskRepositoryMock.Verify(repo => repo.DeleteAsync(taskId), Times.Once);
        }
    }
}
