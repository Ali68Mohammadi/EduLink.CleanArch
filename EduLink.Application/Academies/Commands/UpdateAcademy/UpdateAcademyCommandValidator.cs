using FluentValidation;

namespace EduLink.Application.Academies.Commands.UpdateAcademy;

public class UpdateAcademyCommandValidator : AbstractValidator<UpdateAcademyCommand>
{

    public UpdateAcademyCommandValidator()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .Length(3, 500)
            .NotEmpty().WithMessage("Descriptaion is required!");


    }
}