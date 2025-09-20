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
    }
}
