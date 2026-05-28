using EduLink.Application.Academies.Commands.CreateAcademy;
using FluentAssertions;
using FluentValidation.TestHelper;

namespace EduLink.Application.UnitTests.Academy.Commands.CreateAcdemy;


public class CreateAcademyCommandValidatorTests
{

    [Fact]
    public void Validator_WithValidCommand_ShouldNotHaveValidationError()
    {
        // Arrange
        var command = new CreateAcademyCommand
        {
            Name = "Summer Academy",
            Description = "This is a description of the academy.",
            Category = "Art",
            ContactEmail = "test@test.com",
            PostalCode = "12345",
            City = "Berlin"
        };
        var validator = new CreateAcademyCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }


    [Fact]
    public void Validator_WithInvalidCommand_ShouldHaveValidationError()
    {
        // Arrange
        var command = new CreateAcademyCommand
        {
            Name = "as",
            Description = "de",
            Category = "sport",
            ContactEmail = "testtest.com",
            PostalCode = "123456",
            City = ""
        };
        var validator = new CreateAcademyCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Description);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        result.ShouldHaveValidationErrorFor(c => c.City);


    }


    [Theory()]
    [InlineData("IT")]
    [InlineData("Business")]
    [InlineData("Languages")]
    [InlineData("Art")]
    [InlineData("PersonalDevelopment")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorForCategoryProperty(string category)
    {
        // Arrange
        var command = new CreateAcademyCommand { Category = category, };
        var validator = new CreateAcademyCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);


    }

    [Theory]
    [InlineData("123")]     
    [InlineData("123456")]   
    [InlineData("abcde")]    
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationError(string postalCode)
    {
        // Arrange
        var command = new CreateAcademyCommand { PostalCode = postalCode,  };
        var validator = new CreateAcademyCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

    [Theory]
    [InlineData(null)]       
    [InlineData("")]         
    [InlineData("12345")]   
    public void Validator_ForValidOrEmptyPostalCode_ShouldNotHaveValidationError(string? postalCode)
    {
        // Arrange
        var command = new CreateAcademyCommand { PostalCode = postalCode,  };
        var validator = new CreateAcademyCommandValidator();

        // Act
        var result = validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.PostalCode);
    }
}