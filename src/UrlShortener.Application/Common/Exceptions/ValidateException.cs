using FluentValidation;
using FluentValidation.Results;

namespace UrlShortener.Application.Common.Exceptions;

[Serializable]
public class ValidateException : ValidationException
{
    public ValidateException(string message) : base(message)
    {
    }

    public ValidateException(string message, Exception innerException) : base(message, (IEnumerable<ValidationFailure>)innerException)
    {
    }

    public ValidateException(string message, IEnumerable<ValidationFailure> errors, bool appendDefaultMessage)
        : base(appendDefaultMessage ? $"{message} {errors}" : message)
    {
    }

    public ValidateException(IEnumerable<ValidationFailure> errors) : base(errors)
    {
    }
}