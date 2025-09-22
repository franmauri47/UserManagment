using Application.Users.Commands;
using Domain.Helpers;
using FluentValidation;

namespace Application.Users.Validators;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator()
    {
        RuleFor(x => x.data.Name)
            .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
            .MaximumLength(250).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);
        RuleFor(x => x.data.Email)
            .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
            .EmailAddress().WithMessage(ValidationHelpers.MessageInvalidFieldByName)
            .MaximumLength(320).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);

        When(x => x.data.DomicileData != null, () =>
        {
            RuleFor(x => x.data.DomicileData!.Street)
                .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
                .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);
            RuleFor(x => x.data.DomicileData!.Province)
                .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
                .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);
            RuleFor(x => x.data.DomicileData!.City)
                .NotEmpty().WithMessage(ValidationHelpers.MessageRequiredFieldByName)
                .MaximumLength(100).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName);
            RuleFor(x => x.data.DomicileData!.DirectionNumber)
                .MaximumLength(10).WithMessage(ValidationHelpers.MessageMaxLengthFieldByName)
                .When(d => !string.IsNullOrEmpty(d.data.DomicileData!.DirectionNumber));
        });
    }
}
