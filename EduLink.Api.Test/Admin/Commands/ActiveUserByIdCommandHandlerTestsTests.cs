using EduLink.Application.Admin.Commands.ActiveUserById;
using EduLink.Domain.Entities; 
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Moq;


namespace EduLink.Test.Admin.Commands
{
    public class ActiveUserByIdCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock = new();
        private readonly Mock<ILogger<ActiveUserByIdCommandHandler>> _loggerMock = new();
        private readonly ActiveUserByIdCommandHandler _handler;

        public ActiveUserByIdCommandHandlerTests()
        {
            _handler = new ActiveUserByIdCommandHandler(_userRepositoryMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_UserExists_ShouldActivateUserAndSaveChanges()
        {
            // Arrange
            var userId =  1;
            var user = new User { Id = userId, IsActive = false };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(user);

            // Act
            await _handler.Handle(new ActiveUserByIdCommand(userId), CancellationToken.None);

            // Assert
            Assert.True(user.IsActive);
            _userRepositoryMock.Verify(repo => repo.SaveChangesAsync(), Moq.Times.Once);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_ShouldThrowNotFoundException()
        {
            // Arrange
            var userId = 999;
            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync((User?)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() =>
                _handler.Handle(new ActiveUserByIdCommand(userId), CancellationToken.None));
        }
    }
}