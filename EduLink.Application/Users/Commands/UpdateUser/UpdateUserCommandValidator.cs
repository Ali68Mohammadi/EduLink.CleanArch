using EduLink.Application.Users.Commands.UpdateUser;
using FluentValidation;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {

        RuleFor(x => x.FirstName)
               .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.FirstName));

        RuleFor(x => x.LastName)
            .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.LastName));

        RuleFor(x => x.PhoneNumber)
            .Matches(@"^09\d{10}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber));
    }
}