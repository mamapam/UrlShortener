using FluentValidation;
using UrlShortener.Application.UrlApplication.Commands;

namespace UrlShortener.Application.Validators;

public class CreateShortenedUrlCommandValidator : AbstractValidator<CreateShortenedUrlCommand>
{
    public CreateShortenedUrlCommandValidator()
    {
        RuleFor(u => u.Url).NotEmpty();
        RuleFor(u => u.Url)
                .Must((createShortenedUrl, cancellationToken) => IsValidUrl(createShortenedUrl.Url))
                .WithMessage("Invalid URL provided.");
    }

    private bool IsValidUrl(string url)
    {
        if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
        {
            var uri = new Uri(url);
            if (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps)
            {
                return true;
            }
        }

        return false;
    }
}