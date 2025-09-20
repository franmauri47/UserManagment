using FluentValidation.Results;

namespace Application.Common.Exceptions;

[Serializable]
public class ValidatorException : Exception
{
    public ValidatorException()
        :base("One or more validation failures have occurred.")
    {
    }

    public ValidatorException(IEnumerable<ValidationFailure> failures) 
        : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public IDictionary<string, string[]>? Errors { get; } = new Dictionary<string, string[]>();
}
