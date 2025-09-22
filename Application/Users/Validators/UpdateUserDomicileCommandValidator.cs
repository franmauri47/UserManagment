using Application.Users.Commands;
using Domain.Helpers;
using FluentValidation;

namespace Application.Users.Validators;

public class UpdateUserDomicileCommandValidator : AbstractValidator<UpdateUserDomicileCommand>
{
    public UpdateUserDomicileCommandValidator()
    {
        RuleFor(x => x.domicileData)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName);

        RuleFor(x => x.domicileData.Province)
            .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
            .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);

        RuleFor(x => x.domicileData.City)
            .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
            .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);

        RuleFor(x => x.domicileData.Street)
            .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
            .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);

        RuleFor(x => x.domicileData.DirectionNumber)
            .MaximumLength(10).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName)
            .When(d => !string.IsNullOrEmpty(d.domicileData.DirectionNumber));
    }
}
