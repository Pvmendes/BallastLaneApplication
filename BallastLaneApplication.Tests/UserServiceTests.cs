using BallastLaneApplication.Core.Dtos;
using BallastLaneApplication.Core.Entities;
using BallastLaneApplication.Infrastructure.Interfaces.Repository;
using BallastLaneApplication.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BallastLaneApplication.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private UserService _userService;

        [SetUp]
        public void Setup()
        {
            // Mock the UserRepository
            _userRepositoryMock = new Mock<IUserRepository>();

            // Initialize UserService with the mocked repository
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Test]
        public async Task GetUserByIdAsync_UserExists_ReturnsUserAsync()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedUser = new UserEntitie { Id = userId, Username = "testuser" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetUserByIdAsync(userId);

            // Assert
            expectedUser.Equals(result);
        }

        [Test]
        public async Task GetByUserNameAsync_UserExists_ReturnsUserAsync()
        {
            // Arrange
            var userId = "testuser";
            var expectedUser = new UserEntitie { Id = Guid.NewGuid(), Username = userId };
            _userRepositoryMock.Setup(repo => repo.GetByUserNameAsync(userId)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.GetByUserNameAsync(userId);

            // Assert
            expectedUser.Equals(result);
        }

        [Test]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsUserAsync()
        {
            // Arrange
            var testUsername = "testuser";
            var testPassword = "testpassword";

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(testPassword);

            var expectedUser = new UserEntitie { Username = testUsername, PasswordHash = passwordHash };
            _userRepositoryMock.Setup(repo => repo.GetByUserNameAsync(testUsername)).ReturnsAsync(expectedUser);

            // Act
            var result = await _userService.AuthenticateAsync(testUsername, testPassword);

            // Assert
            Assert.IsNotNull(result);
            Assert.That(result, Is.EqualTo(expectedUser));
        }

        [Test]
        public async Task AuthenticateAsync_InvalidCredentials_ReturnsNull()
        {
            // Arrange
            var testUsername = "testuser";
            var testPassword = "wrongpassword";
            _userRepositoryMock.Setup(repo => repo.GetByUserNameAsync(testUsername)).ReturnsAsync((UserEntitie)null);

            // Act
            var result = await _userService.AuthenticateAsync(testUsername, testPassword);

            // Assert
            Assert.IsNull(result);
        }

        [Test]
        public async Task UpdateUserAsync_ValidUser_UpdatesUser()
        {
            // Arrange
            var existingUser = new UserEntitie { Id = Guid.NewGuid(), Username = "existinguser", PasswordHash = "oldpassword" };
            var updatedUser = new UserEntitie { Id = existingUser.Id, Username = "existinguser", PasswordHash = "newpassword" };
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(existingUser.Id)).ReturnsAsync(existingUser);
            _userRepositoryMock.Setup(repo => repo.UpdateAsync(updatedUser)).Returns(Task.CompletedTask);

            // Act
            await _userService.UpdateUserAsync(updatedUser);

            // Assert
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.Is<UserEntitie>(u => u.Id == updatedUser.Id && u.PasswordHash == updatedUser.PasswordHash)), Times.Once);
        }

        [Test]
        public async Task DeleteUserAsync_UserExists_DeletesUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            _userRepositoryMock.Setup(repo => repo.DeleteAsync(userId)).Returns(Task.CompletedTask);

            // Act
            await _userService.DeleteUserAsync(userId);

            // Assert
            _userRepositoryMock.Verify(repo => repo.DeleteAsync(userId), Times.Once);
        }
    }
}
