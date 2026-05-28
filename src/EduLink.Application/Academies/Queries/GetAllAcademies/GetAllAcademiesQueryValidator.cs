using EduLink.Application.Academies.Dtos;
using FluentValidation;

namespace EduLink.Application.Academies.Queries.GetAllAcademies;

public class GetAllAcademiesQueryValidator : AbstractValidator<GetAllAcademiesQuery>
{
    private int[] allowPagesSizes = [5, 10, 15, 30];
    private string[] allowSortByColumnNames = [nameof(AcademyDto.Name),
        nameof(AcademyDto.Category),
        nameof(AcademyDto.Description)];

    public GetAllAcademiesQueryValidator()
    {

        RuleFor(r => r.PageNumber)
            .GreaterThanOrEqualTo(1);

        RuleFor(r => r.PageSize)
            .Must(value => allowPagesSizes.Contains(value))
            .WithMessage($"Page size must be in [{string.Join(",", allowPagesSizes)}]");

        RuleFor(r => r.SortBy)
            .Must(value => allowSortByColumnNames.Contains(value))
            .When(q => q.SortBy != null)
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowSortByColumnNames)}]");
    }
}
