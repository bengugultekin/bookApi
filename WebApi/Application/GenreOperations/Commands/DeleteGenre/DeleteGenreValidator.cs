using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands;

public class DeleteGenreValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreValidator()
    {
        RuleFor(command => command.GenreId).GreaterThan(0);
    }
}
