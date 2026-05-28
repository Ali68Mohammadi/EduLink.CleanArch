using EduLink.Application.Users;
using EduLink.Domain.Constants;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace EduLink.Application.UnitTests.Users;

public class UserContextTests
{


    [Fact]
    public void GetCurrenUser_WhenUserIsAuthenticated_ReturnsCurrenUser()
    {
        // Arrange
        var dateOfBirth = new DateOnly(1990, 1, 1);
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, "123"),
            new (ClaimTypes.Email, "test@test.com"),
            new (ClaimTypes.Role,UserRoles.Admin),
           new (ClaimTypes.Role,UserRoles.User),
            new("Nationality","German"),
            new ("DateOfBirth", dateOfBirth.ToString("yyyy-MM-dd"))

        };

        var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));

        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
        {
            User = user
        });

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //act
        var currentUser = userContext.GetCurrenUser();

        //assert

        currentUser.Should().NotBeNull();
        currentUser.Id.Should().Be("123");
        currentUser.Email.Should().Be("test@test.com");
        currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        currentUser.Nationality.Should().Be("German");
        currentUser.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact]
    public void GetCurrenUser_WithUserContextNotPresent_throwInvalidOperationException()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

        var userContext = new UserContext(httpContextAccessorMock.Object);

        //act   
        Action action = () => userContext.GetCurrenUser();
        //Assert
        action.Should()
            .Throw<InvalidOperationException>()
            .WithMessage("User context is not present!");


    }
}

