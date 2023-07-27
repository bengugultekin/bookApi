using FluentValidation;

namespace WebApi.Application.GenreOperations.Commands;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(command => command.Model.Name.Trim()).NotEmpty().MinimumLength(4).When(x => x.Model.Name.Trim() != string.Empty);
    }
}
