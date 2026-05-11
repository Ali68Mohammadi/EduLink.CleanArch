using FluentValidation;

namespace EduLink.Application.Academies.Commands.CreateAcademy;


public class CreateAcademyCommandValidator : AbstractValidator<CreateAcademyCommand>
{
    private readonly List<string> validCategories =
    ["Programming", "Business", "Languages", "Art", "PersonalDevelopment"];

    public CreateAcademyCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .Length(3, 500)
            .NotEmpty().WithMessage("Descriptaion is required!");

        RuleFor(dto => dto.Category)
            .Must( validCategories.Contains)
            .WithMessage("Invalid Category. Please choose from the Valid Categories! ");

        //or :
        //.Custom((value, context) =>
        //{
        //    var isValidCategory = validCategories.Contains(value);
        //    if (!isValidCategory)
        //    {
        //        context.AddFailure("Category","Invalid Category. Please choose from the Valid Categories! ");
        //    }
        //});


        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide a valid Email Address!");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{5}$").WithMessage("Postal Code must be exactly 5 digits.")
            .When(x => !string.IsNullOrEmpty(x.PostalCode));

        RuleFor(dto => dto.City)
            .NotEmpty();


    }
}

