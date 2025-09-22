using Application.Users.Queries;
using Domain.Helpers;
using FluentValidation;

namespace Application.Users.Validators;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Name)
            .MaximumLength(250).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName)
            .When(e => !string.IsNullOrEmpty(e.Name));

        RuleFor(x => x.City)
            .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName)
            .When(e => !string.IsNullOrEmpty(e.City));

        RuleFor(x => x.Province)
            .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName)
            .When(e => !string.IsNullOrEmpty(e.Province));
    }
}
