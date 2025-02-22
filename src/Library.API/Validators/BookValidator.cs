using FluentValidation;
using Library.Models.Entities;

namespace Library.API.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(b => b.Author)
            .NotEmpty()
            .NotNull()
            .WithMessage("Author name should be specified");

        RuleFor(b => b.PublishedYear)
            .Must(year => year >= 0 && year <= DateTime.Now.Year)
            .WithMessage($"Year should be between 0 and {DateTime.Now.Year}");

        RuleFor(b => b.Title)
            .NotNull()
            .NotEmpty()
            .WithMessage("Title should be specified");
    }
}
