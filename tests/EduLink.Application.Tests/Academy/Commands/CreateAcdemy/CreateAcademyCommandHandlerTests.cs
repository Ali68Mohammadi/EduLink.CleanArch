using AutoMapper;
using EduLink.Application.Academies.Commands.CreateAcademy;
using EduLink.Application.Users;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace EduLink.Application.UnitTests.Academy.Commands.CreateAcdemy;

public class CreateAcademyCommandHandlerTests
{
    [Fact]
    public async Task Handle_ForValidCommand_ReturnsCreatedAcademyId()
    {
        // Arrange
        var loggerMock = new Mock<ILogger<CreateAcademyCommandHandler>>();
        var mapperMock = new Mock<IMapper>();

        var command = new CreateAcademyCommand();
        var academy = new Domain.Entities.Academy();

        mapperMock.Setup(m => m.Map<Domain.Entities.Academy>(command))
            .Returns(academy); // Simulate mapping the command to an Academy entity

        var academiesRepositoryMock = new Mock<IAcademiesRepository>();
        academiesRepositoryMock.Setup(repo => repo.Create(It.IsAny<Domain.Entities.Academy>()))
            .ReturnsAsync(1); // Simulate returning the created academy ID


        var userContextMock = new Mock<IUserContext>();

        var currentUser = new CurrenUser("manager_id", "test@test.com", [], null, null);
        userContextMock.Setup(uc => uc.GetCurrenUser()).Returns(currentUser);


        var commandHandler = new CreateAcademyCommandHandler(
            academiesRepositoryMock.Object,
            loggerMock.Object,
            userContextMock.Object,
            mapperMock.Object);

        //act

        var result = await commandHandler.Handle(command, CancellationToken.None);

        //assert
        result.Should().Be(1); // Assert  that the returned academy ID is correct
        academy.ManagerId.Should().Be("manager_id"); // Assert that the ManagerId is set correctly
        academiesRepositoryMock.Verify   // Verify that the Create method was called once
                        (repo => repo.Create(academy), Times.Once);



    }
}
