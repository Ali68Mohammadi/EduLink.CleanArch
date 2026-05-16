using EduLink.Application.Academies.Commands.CreateAcademy;
using FluentValidation;

namespace EduLink.Application.Courses.Command.CreateCourse;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
   
        public CreateCourseCommandValidator()
        {
            RuleFor(x => x.AcademyId).NotEmpty().GreaterThan(0);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .Length(3, 100);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required")
                .Length(3, 500);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negativ number.");
        }
    

}



