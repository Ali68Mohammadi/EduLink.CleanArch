using AutoMapper;
using EduLink.Application.Academies.Commands.UpdateAcademy;
using EduLink.Application.Users;
using EduLink.Domain.Constants;
using EduLink.Domain.Entities;
using EduLink.Domain.Exceptions;
using EduLink.Domain.Interfaces;
using EduLink.Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace EduLink.Application.UnitTests.Academy.Commands.UpdateAcademy;


public class UpdateAcademyCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateAcademyCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IAcademiesRepository> _academiesRepositoryMock;
    private readonly Mock<IAcademyAuthorizationService> _academyAuthorizationServiceMock;
    private readonly UpdateAcademyCommandHandler _handler;

    public UpdateAcademyCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateAcademyCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _academyAuthorizationServiceMock = new Mock<IAcademyAuthorizationService>();
        _academiesRepositoryMock = new Mock<IAcademiesRepository>();
        _handler = new UpdateAcademyCommandHandler(_academiesRepositoryMock.Object, _loggerMock.Object,
            _mapperMock.Object, _academyAuthorizationServiceMock.Object);
    }

    [Fact]
    public async Task Handle_WithValidRequest_ShouldUpdateAcademy()
    {
        // Arrange
        var academyId = 1;
        var command = new UpdateAcademyCommand
        {
            Id = academyId,
            Name = "new Test",
            Description = "New Description",
            IsOnline = true,
        };

        var academy = new Domain.Entities.Academy()
        {
            Id = academyId,
            Name = "Test",
            Description = "Test Description",
        };

        // Fix: set up the repository to return the academy when GetByIdAsync(1) is called
        _academiesRepositoryMock.Setup(re => re.GetByIdAsync(academyId)).ReturnsAsync(academy);

        _academyAuthorizationServiceMock.Setup(p => p.Authorize(academy, ResourceOperationEnm.Update))
            .Returns(true);

        //act
        await _handler.Handle(command, CancellationToken.None);

        //assert
        _academiesRepositoryMock.Verify(a => a.SaveChangesAsync(), Times.Once());

        _mapperMock.Verify(m => m.Map(command, academy), Times.Once());
    }


    [Fact]
    public async Task Handle_WithNonExistingAcademy_ShouldThrowNotFoundException()
    {
        //arrange
        var academyId = 2;
        var request = new UpdateAcademyCommand
        {
            Id = academyId,
        };

        _academiesRepositoryMock.Setup(re => re.GetByIdAsync(academyId))
            .ReturnsAsync((Domain.Entities.Academy?)null);

        //act 
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);


        //Assert
        await act.Should().ThrowAsync<NotFoundException>();


    }


    [Fact]
    public async Task Handle_WithUnaunthorizedUser_ShouldThrowForbidenException()
    {
        //arrange
        var academyId = 3;
        var request = new UpdateAcademyCommand
        {
            Id = academyId,
        };

        var existingAcademy = new Domain.Entities.Academy
        {
            Id = academyId,
        };

        _academiesRepositoryMock.Setup(re => re.GetByIdAsync(academyId))
            .ReturnsAsync(existingAcademy);

        _academyAuthorizationServiceMock.Setup(p => p.Authorize(existingAcademy, ResourceOperationEnm.Update))
            .Returns(false);

        //act 
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);


        //Assert
        await act.Should().ThrowAsync<ForbidExeption>();
    }
}