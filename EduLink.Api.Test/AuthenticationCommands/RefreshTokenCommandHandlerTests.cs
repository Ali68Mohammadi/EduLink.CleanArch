
using EduLink.Application.Abstractions;
using EduLink.Application.Authentication.Commands.RefreshToken;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EduLink.Test.AuthenticationCommands;

public class RefreshTokenCommandHandlerTests
{
    private readonly Mock<IAuthRepository> _authRepository = new();
    private readonly Mock<IJwtProvider> _jwtProvider = new();
    private readonly IConfiguration _configuration;
    private readonly RefreshTokenCommandHandler _handler;

    public RefreshTokenCommandHandlerTests()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            {"Jwt:RefreshTokenDurationInDays", "30"},
            {"Jwt:DurationInMinutes", "60"}
        };

        _configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _handler = new RefreshTokenCommandHandler(
            _authRepository.Object,
            _configuration,
            _jwtProvider.Object);
    }

    [Fact]
    public async Task Handle_WhenUserDoesNotExist_ShouldThrowNotFoundException()
    {
        // ۱. Arrange 
        var refreshToken = "fake-token";
        var command = new RefreshTokenCommand(refreshToken);

        _authRepository.Setup(repo => repo.GetByRefreshTokenAsync(refreshToken))
            .ReturnsAsync((User?)null);

        // ۲. Act & ۳. Assert 
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }


    [Fact]
    public async Task Handle_WhenUserIsInactive_ShouldThrowBadRequestException()
    {
        // Arrange
        var refreshToken = "invalid-token";
        var command = new RefreshTokenCommand(refreshToken);
        User inactiveUser = new() { Id = 1, IsActive = false };

        _authRepository.Setup(rep => rep.GetByRefreshTokenAsync(refreshToken))
                       .ReturnsAsync(inactiveUser);

        // Act & Assert 
        await Assert.ThrowsAsync<BadRequestException>(() =>
                 _handler.Handle(command, CancellationToken.None));
    }


    [Fact]
    public async Task Handle_WhenRequestIsValid_ShouldReturnNewTokensAndSaveToDatabase()
    {
        //Arrange 
        string oldToken = "old-token";
        var newToken = "new-token-123";
        var command = new RefreshTokenCommand(oldToken);

        User user = new()
        {
            Id = 1,
            IsActive = true,
            RefreshToken = oldToken,
        };

        _authRepository.Setup(r => r.GetByRefreshTokenAsync(oldToken))
            .ReturnsAsync(user);

        _jwtProvider.Setup(r => r.Generate(user))
            .Returns(newToken);

        //Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert

        Assert.NotNull(result);
        Assert.Equal(newToken, result.AccessToken);

        _authRepository.Verify(r => r.SaveChangesAsync(), Times.Once);
    }


}

