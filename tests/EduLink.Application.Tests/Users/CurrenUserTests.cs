using EduLink.Application.Users;
using EduLink.Domain.Constants;
using FluentAssertions;

namespace EduLink.Application.UnitTests.Users
{
    public class CurrenUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRole_WithMatchingRole_ReturnsTrue(string roleName)
        {
            // Arrange
            var user = new CurrenUser("1", "test@test.com", [nameof(UserRoles.Admin), UserRoles.User], null, null);

            // Act
            var isInRole = user.IsInRole(roleName);

            // Assert
            isInRole.Should().BeTrue();
        }

        [Fact]
        public void IsInRole_WithNoMatchingRole_ReturnsFalse()
        {
            // Arrange
            var user = new CurrenUser("1", "email@example.com", [UserRoles.Admin, UserRoles.User], null, null);

            // Act
            bool isInRole = user.IsInRole(UserRoles.Manager);

            // Assert
            isInRole.Should().BeFalse();
        }


        [Fact]
        public void IsInRole_WithNoMatchingRoleCase_ReturnsFalse()
        {
            // Arrange
            var user = new CurrenUser("1", "email@example.com", [UserRoles.Admin, UserRoles.User], null, null);

            // Act
            bool isInRole = user.IsInRole(UserRoles.Admin.ToLower());

            // Assert
            isInRole.Should().BeFalse();
        }



        //[Fact]
        //public void IsInRole_RoleNotExists_ReturnsFalse()
        //{
        //    // Arrange
        //    var roles = new List<string> { "User", "Viewer" };
        //    var user = new CurrenUser("id", "email@example.com", roles, null, null);

        //    // Act
        //    bool result = user.IsInRole("Admin");

        //    // Assert
        //    Assert.False(result);
        //}

        //[Fact]
        //public void IsInRole_NullRolePresent_ReturnsTrue()
        //{
        //    // Arrange
        //    //var roles = new Ie<string> ();
        //    var roles = new List<string?> { null } as IEnumerable<string>;
        //    var user = new CurrenUser("id", "email@example.com", roles, null, null);

        //    // Act
        //    bool result = user.IsInRole(null!);

        //    // Assert
        //    Assert.True(result);
        //}

        //[Fact]
        //public void IsInRole_NullRoleAbsent_ReturnsFalse()
        //{
        //    // Arrange
        //    var roles = new List<string> { "Admin" };
        //    var user = new CurrenUser("id", "email@example.com", roles, null, null);

        //    // Act
        //    bool result = user.IsInRole(null!);

        //    // Assert
        //    Assert.False(result);
        //}

        //[Fact]
        //public void IsInRole_RolesIsNull_ThrowsArgumentNullException()
        //{
        //    // Arrange
        //    IEnumerable<string>? roles = null;
        //    var user = new CurrenUser("id", "email@example.com", roles!, null, null);

        //    // Act & Assert
        //    Assert.Throws<ArgumentNullException>(() => user.IsInRole("Admin"));
        //}
    }
}
