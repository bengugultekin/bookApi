using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(command => command.model.FirstName).MinimumLength(4);
        RuleFor(command => command.model.LastName).MinimumLength(4);
        RuleFor(command => command.model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
    }
}
