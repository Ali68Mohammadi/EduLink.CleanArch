using EduLink.Application.Users;
using EduLink.Domain.Entities;
using EduLink.Domain.Repositories;
using EduLink.Infrastructure.Authorization.Requirements.CreateMinimumAcademiesRequirement;
using EduLink.Infrastructure.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;

namespace EduLink.Infrastructure.Tests.Authorization.Requirements;


public class CreateMinimumAcademiesRequirementHandlerTest
{
    [Fact]
    public async Task HandleRequirementAsync_UserHatCretaedMinimumAcademies_ShouldSucced()
    {
        // Arrange
        var currentUser = new CurrenUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrenUser()).Returns(currentUser);

        var academies = new List<Academy>
        {
            new() { ManagerId = currentUser.Id },
            new() { ManagerId = currentUser.Id },
            new() { ManagerId = "2" },
        };

        var academyRepositoryMock = new Mock<IAcademiesRepository>();
        academyRepositoryMock.Setup(a => a.GetAllAsync()).ReturnsAsync(academies);

        var requirement = new CreateMinimumAcademiesRequirement(2);

        var handler = new CreateMinimumAcademiesRequirementHandler(
            academyRepositoryMock.Object, userContextMock.Object);

        var contetxt = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        await handler.HandleAsync(contetxt);

        // Assert
        contetxt.HasSucceeded.Should().BeTrue();
    }

    [Fact]
    public async Task HandleRequirementAsync_UserHatNotCretaedMinimumAcademies_ShouldFail()
    {
        // Arrange
        var currentUser = new CurrenUser("1", "test@test.com", [], null, null);
        var userContextMock = new Mock<IUserContext>();
        userContextMock.Setup(x => x.GetCurrenUser()).Returns(currentUser);

        var academies = new List<Academy>
        {
            new() { ManagerId = currentUser.Id },
            new() { ManagerId = "2" },
        };

        var academyRepositoryMock = new Mock<IAcademiesRepository>();
        academyRepositoryMock.Setup(a => a.GetAllAsync()).ReturnsAsync(academies);

        var requirement = new CreateMinimumAcademiesRequirement(2);

        var handler = new CreateMinimumAcademiesRequirementHandler(
            academyRepositoryMock.Object, userContextMock.Object);

        var contetxt = new AuthorizationHandlerContext([requirement], null, null);

        // Act
        await handler.HandleAsync(contetxt);

        // Assert
        contetxt.HasSucceeded.Should().BeFalse();
        contetxt.HasFailed.Should().BeTrue();
    }
}
